using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.Diagnostics.AddDiagnosticChoice;
using SDM.Application.Diagnostics.AddDiagnosticQuestion;
using SDM.Application.Diagnostics.AddDiagnosticRule;
using SDM.Application.Diagnostics.CreateDiagnosticCategory;
using SDM.Application.Diagnostics.DeleteDiagnosticChoice;
using SDM.Application.Diagnostics.DeleteDiagnosticQuestion;
using SDM.Application.Diagnostics.DeleteDiagnosticRule;
using SDM.Application.Diagnostics.GetDiagnosticCategories;
using SDM.Application.Diagnostics.GetDiagnosticCategory;
using SDM.Application.Diagnostics.UpdateDiagnosticCategory;
using SDM.Application.Diagnostics.UpdateDiagnosticChoice;
using SDM.Application.Diagnostics.UpdateDiagnosticQuestion;
using SDM.Application.Diagnostics.UpdateDiagnosticRule;
using SDM.Application.Diagnostics.UpdateDiagnosticStatus;

namespace SDM.API.Controllers;

/// <summary>
/// Exposes the offline MCQ diagnostic tree to customers and administrators.
/// Customers can read active categories and their full question/choice/rule tree.
/// Admins can build and maintain the tree.
/// </summary>
public sealed class DiagnosticsController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated list of diagnostic problem categories.
    /// Customers receive only active ones; admins may filter by IsActive flag.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetDiagnosticCategoriesResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> GetDiagnosticCategories([FromQuery] GetDiagnosticCategoriesQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves the full hierarchical tree (category + questions + choices + rules) for a single category.
    /// Consumed by the customer device to run offline diagnostics.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetDiagnosticCategory))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<GetDiagnosticCategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetDiagnosticCategory([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetDiagnosticCategoryQuery { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Creates a new top-level diagnostic problem category.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateDiagnosticCategoryResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateDiagnosticCategory([FromBody] CreateDiagnosticCategoryCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetDiagnosticCategory),
                new { id = result.Data.Id },
                new ApiResponse<CreateDiagnosticCategoryResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Updates the metadata (name, description, icon) of an existing diagnostic category.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateDiagnosticCategory([FromRoute] Guid id, [FromBody] UpdateDiagnosticCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The category ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticCategory.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Activates or deactivates a diagnostic category for customer visibility.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateDiagnosticStatus([FromRoute] Guid id, [FromBody] UpdateDiagnosticStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The category ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticCategory.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Adds a new MCQ question (with all its choices) to an existing diagnostic category.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPost("{categoryId:guid}/questions")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<AddDiagnosticQuestionResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> AddDiagnosticQuestion([FromRoute] Guid categoryId, [FromBody] AddDiagnosticQuestionCommand command)
    {
        if (categoryId != command.CategoryId)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The category ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticQuestion.IdMismatch", "URL category ID and body category ID must match.")]));
        }

        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetDiagnosticCategory),
                new { id = categoryId },
                new ApiResponse<AddDiagnosticQuestionResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Updates the text and ordering of an existing MCQ question.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("questions/{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateDiagnosticQuestion([FromRoute] Guid id, [FromBody] UpdateDiagnosticQuestionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The question ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticQuestion.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Permanently deletes a question and its associated answer choices.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("questions/{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDiagnosticQuestion([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteDiagnosticQuestionCommand { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Adds a new scoring rule to an existing diagnostic category.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPost("{categoryId:guid}/rules")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<AddDiagnosticRuleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> AddDiagnosticRule([FromRoute] Guid categoryId, [FromBody] AddDiagnosticRuleCommand command)
    {
        if (categoryId != command.CategoryId)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The category ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticRule.IdMismatch", "URL category ID and body category ID must match.")]));
        }

        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetDiagnosticCategory),
                new { id = categoryId },
                new ApiResponse<AddDiagnosticRuleResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Updates the score bounds, result, and solution of an existing rule.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("rules/{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateDiagnosticRule([FromRoute] Guid id, [FromBody] UpdateDiagnosticRuleCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The rule ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticRule.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Permanently deletes a diagnostic scoring rule.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("rules/{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDiagnosticRule([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteDiagnosticRuleCommand { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Adds a single new answer choice to an existing diagnostic question.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPost("questions/{questionId:guid}/choices")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<AddDiagnosticChoiceResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> AddDiagnosticChoice([FromRoute] Guid questionId, [FromBody] AddDiagnosticChoiceCommand command)
    {
        if (questionId != command.QuestionId)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The question ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticChoice.IdMismatch", "URL question ID and body question ID must match.")]));
        }

        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return Created(
                uri: string.Empty,
                value: new ApiResponse<AddDiagnosticChoiceResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Updates the text and score value of an existing diagnostic choice.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("choices/{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateDiagnosticChoice([FromRoute] Guid id, [FromBody] UpdateDiagnosticChoiceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The choice ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("DiagnosticChoice.IdMismatch", "URL choice ID and body choice ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Permanently deletes an existing diagnostic choice.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("choices/{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDiagnosticChoice([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteDiagnosticChoiceCommand { Id = id });
        return HandleResult(result);
    }
}
