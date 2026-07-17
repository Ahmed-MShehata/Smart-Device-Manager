using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.GetDiagnosticCategory;

/// <summary>
/// Handles <see cref="GetDiagnosticCategoryQuery"/> via <see cref="IReadDbContext"/>.
/// Assembles the full tree using three targeted, non-tracking SQL queries to avoid N+1.
/// </summary>
public sealed class GetDiagnosticCategoryHandler : IQueryHandler<GetDiagnosticCategoryQuery, GetDiagnosticCategoryResponse>
{
    private readonly IReadDbContext _db;

    /// <summary>Initializes a new instance of <see cref="GetDiagnosticCategoryHandler"/>.</summary>
    public GetDiagnosticCategoryHandler(IReadDbContext db) => _db = db;

    /// <inheritdoc/>
    public async Task<Result<GetDiagnosticCategoryResponse>> Handle(
        GetDiagnosticCategoryQuery query,
        CancellationToken cancellationToken)
    {
        // 1. Load the category
        var category = await _db.DiagnosticCategories
            .Where(c => c.Id == query.Id)
            .Select(c => new { c.Id, c.Name, c.Description, c.IconName, c.IsActive })
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
            return Result<GetDiagnosticCategoryResponse>.Failure(Error.NotFound("DiagnosticCategory"));

        // 2. Load all questions for this category in a single query
        var questions = await _db.DiagnosticQuestions
            .Where(q => q.CategoryId == query.Id)
            .OrderBy(q => q.OrderIndex)
            .Select(q => new { q.Id, q.QuestionText, q.OrderIndex })
            .ToListAsync(cancellationToken);

        // 3. Load all choices for questions in this category in a single query (avoids N+1)
        var questionIds = questions.Select(q => q.Id).ToList();
        var choices = await _db.DiagnosticChoices
            .Where(c => questionIds.Contains(c.QuestionId))
            .Select(c => new { c.Id, c.QuestionId, c.ChoiceText, c.ScoreValue })
            .ToListAsync(cancellationToken);

        // 4. Load all rules for this category in a single query
        var rules = await _db.DiagnosticRules
            .Where(r => r.CategoryId == query.Id)
            .OrderBy(r => r.MinScore)
            .Select(r => new DiagnosticRuleDto
            {
                Id       = r.Id,
                MinScore = r.MinScore,
                MaxScore = r.MaxScore,
                Result   = r.Result,
                Solution = r.Solution
            })
            .ToListAsync(cancellationToken);

        // 5. Assemble the tree in-memory
        var questionDtos = questions.Select(q => new DiagnosticQuestionDto
        {
            Id           = q.Id,
            QuestionText = q.QuestionText,
            OrderIndex   = q.OrderIndex,
            Choices      = choices
                .Where(c => c.QuestionId == q.Id)
                .Select(c => new DiagnosticChoiceDto
                {
                    Id         = c.Id,
                    ChoiceText = c.ChoiceText,
                    ScoreValue = c.ScoreValue
                })
                .ToList()
        }).ToList();

        var response = new GetDiagnosticCategoryResponse
        {
            Id          = category.Id,
            Name        = category.Name,
            Description = category.Description,
            IconName    = category.IconName,
            IsActive    = category.IsActive,
            Questions   = questionDtos,
            Rules       = rules
        };

        return Result<GetDiagnosticCategoryResponse>.Success(response);
    }
}
