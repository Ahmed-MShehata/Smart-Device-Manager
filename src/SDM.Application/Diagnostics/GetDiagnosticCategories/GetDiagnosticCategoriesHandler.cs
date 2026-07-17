using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.GetDiagnosticCategories;

/// <summary>
/// Handles <see cref="GetDiagnosticCategoriesQuery"/> via <see cref="IReadDbContext"/>.
/// Uses sub-query COUNT projections to avoid N+1.
/// </summary>
public sealed class GetDiagnosticCategoriesHandler : IQueryHandler<GetDiagnosticCategoriesQuery, PaginationResponse<GetDiagnosticCategoriesResponse>>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetDiagnosticCategoriesHandler"/>.</summary>
    public GetDiagnosticCategoriesHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<PaginationResponse<GetDiagnosticCategoriesResponse>>> Handle(
        GetDiagnosticCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        var q = _db.DiagnosticCategories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
            q = q.Where(c => EF.Functions.Like(c.Name, $"%{query.Search}%"));

        if (query.IsActive.HasValue)
            q = q.Where(c => c.IsActive == query.IsActive.Value);

        q = q.OrderBy(c => c.Name);

        var totalCount = await q.CountAsync(cancellationToken);

        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new GetDiagnosticCategoriesResponse
            {
                Id            = c.Id,
                Name          = c.Name,
                Description   = c.Description,
                IconName      = c.IconName,
                IsActive      = c.IsActive,
                // Sub-query COUNT to avoid N+1
                QuestionCount = _db.DiagnosticQuestions.Count(q => q.CategoryId == c.Id),
                RuleCount     = _db.DiagnosticRules.Count(r => r.CategoryId == c.Id)
            })
            .ToListAsync(cancellationToken);

        var pagination = PaginationRequest.Create(query.Page, query.PageSize);
        var response = PaginationResponse<GetDiagnosticCategoriesResponse>.Create(items, totalCount, pagination);

        return Result<PaginationResponse<GetDiagnosticCategoriesResponse>>.Success(response);
    }
}
