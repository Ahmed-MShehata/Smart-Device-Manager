using SDM.Application.Interfaces;
using SDM.Domain.Entities;
using SDM.Infrastructure.Persistence;

namespace SDM.Infrastructure.Repositories;

/// <summary>
/// Unit of Work implementation that aggregates all repositories and wraps them
/// in a single <see cref="ApplicationDbContext"/> transaction scope.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IRepository<AdminUser>?            _adminUsers;
    private IRepository<Product>?              _products;
    private IRepository<Order>?                _orders;
    private IRepository<OrderItem>?            _orderItems;
    private IRepository<SoftwarePackage>?      _softwarePackages;
    private IRepository<KnowledgeBaseArticle>? _knowledgeBaseArticles;
    private IRepository<CompanyProfile>?       _companyProfiles;
    private IRepository<DiagnosticCategory>?   _diagnosticCategories;
    private IRepository<DiagnosticQuestion>?   _diagnosticQuestions;
    private IRepository<DiagnosticChoice>?     _diagnosticChoices;
    private IRepository<DiagnosticRule>?       _diagnosticRules;

    /// <summary>Initializes a new instance of <see cref="UnitOfWork"/>.</summary>
    public UnitOfWork(ApplicationDbContext context) => _context = context;

    /// <inheritdoc/>
    public IRepository<AdminUser> AdminUsers
        => _adminUsers ??= new Repository<AdminUser>(_context);

    /// <inheritdoc/>
    public IRepository<Product> Products
        => _products ??= new Repository<Product>(_context);

    /// <inheritdoc/>
    public IRepository<Order> Orders
        => _orders ??= new Repository<Order>(_context);

    /// <inheritdoc/>
    public IRepository<OrderItem> OrderItems
        => _orderItems ??= new Repository<OrderItem>(_context);

    /// <inheritdoc/>
    public IRepository<SoftwarePackage> SoftwarePackages
        => _softwarePackages ??= new Repository<SoftwarePackage>(_context);

    /// <inheritdoc/>
    public IRepository<KnowledgeBaseArticle> KnowledgeBaseArticles
        => _knowledgeBaseArticles ??= new Repository<KnowledgeBaseArticle>(_context);

    /// <inheritdoc/>
    public IRepository<CompanyProfile> CompanyProfiles
        => _companyProfiles ??= new Repository<CompanyProfile>(_context);

    /// <inheritdoc/>
    public IRepository<DiagnosticCategory> DiagnosticCategories
        => _diagnosticCategories ??= new Repository<DiagnosticCategory>(_context);

    /// <inheritdoc/>
    public IRepository<DiagnosticQuestion> DiagnosticQuestions
        => _diagnosticQuestions ??= new Repository<DiagnosticQuestion>(_context);

    /// <inheritdoc/>
    public IRepository<DiagnosticChoice> DiagnosticChoices
        => _diagnosticChoices ??= new Repository<DiagnosticChoice>(_context);

    /// <inheritdoc/>
    public IRepository<DiagnosticRule> DiagnosticRules
        => _diagnosticRules ??= new Repository<DiagnosticRule>(_context);

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    /// <inheritdoc/>
    public void Dispose() => _context.Dispose();
}
