using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.KnowledgeBase.CreateArticle;
using SDM.Application.KnowledgeBase.DeleteArticle;
using SDM.Application.KnowledgeBase.GetArticles;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all HTTP requests for the Knowledge Base.
/// GET (list + single) are anonymous — all write operations require admin authentication.
/// </summary>
public sealed class KnowledgeBaseController : ApiControllerBase
{
    /// <summary>
    /// Retrieves all visible knowledge base articles.
    /// Anonymous — used by the Customer Application.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<List<ArticleDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetArticles(
        [FromQuery] string? category,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(
            new GetArticlesQuery { IncludeHidden = false, Category = category },
            cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves ALL articles including hidden ones.
    /// Admin only — used in the Knowledge Base Management page.
    /// </summary>
    [HttpGet("all")]
    [Authorize(Policy = Policies.RequireAnyAdmin)]
    [ProducesResponseType(typeof(ApiResponse<List<ArticleDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllArticles(
        [FromQuery] string? category,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(
            new GetArticlesQuery { IncludeHidden = true, Category = category },
            cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Creates a new knowledge base article.
    /// Admin only.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateArticleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateArticle(
        [FromBody] CreateArticleCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsSuccess)
            return Created(string.Empty, new ApiResponse<CreateArticleResponse>(true, result.Message, result.Data, []));
        return HandleResult(result);
    }

    /// <summary>
    /// Deletes a knowledge base article.
    /// Admin only.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteArticle(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteArticleCommand { Id = id }, cancellationToken);
        if (result.IsSuccess) return NoContent();
        return HandleResult(result);
    }
}
