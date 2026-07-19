using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.KnowledgeBase.CreateArticle;
using SDM.Application.KnowledgeBase.DeleteArticle;
using SDM.Application.KnowledgeBase.GetArticle;
using SDM.Application.KnowledgeBase.GetArticles;
using SDM.Application.KnowledgeBase.UpdateArticle;

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
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(
            new GetArticlesQuery { IncludeHidden = false, Category = category, Search = search },
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
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(
            new GetArticlesQuery { IncludeHidden = true, Category = category, Search = search },
            cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves a single knowledge base article by its ID.
    /// Anonymous — used by both Admin and Customer applications.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetArticle))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<ArticleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetArticle([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetArticleQuery { Id = id }, cancellationToken);
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
        {
            return CreatedAtRoute(
                nameof(GetArticle),
                new { id = result.Data!.Id },
                new ApiResponse<CreateArticleResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Updates an existing knowledge base article.
    /// Admin only.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateArticle(
        [FromRoute] Guid id,
        [FromBody] UpdateArticleCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The article ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("Article.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Deletes a knowledge base article (physical delete).
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
