using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.Application.Queries.Grape;
using Weindrachen.DTOs.Grape;

namespace Weindrachen.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GrapesController : ControllerBase
{
    private readonly IValidator<GrapeInput> _grapeValidator;
    private readonly IMediator _mediator;

    public GrapesController(IMediator mediator, IValidator<GrapeInput> grapeValidator)
    {
        _mediator = mediator;
        _grapeValidator = grapeValidator;
    }

    /// <summary>
    /// Includes a new Grape
    /// </summary>
    /// <param name="newGrape"></param>
    /// <returns>A newly created Grape</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/Grapes
    ///     {
    ///        "name": "Bobal"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the newly created grape</response>
    /// <response code="400">If Grape is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGrapeAsync(GrapeInput newGrape)
    {
        var validationResult = await _grapeValidator.ValidateAsync(newGrape);

        if (!validationResult.IsValid)
            return BadRequest(string.Join(", ", validationResult.Errors));

        var grape = await _mediator.Send(new AddGrapeCommand(newGrape));
        return grape.Data != null
            ? Ok(grape)
            : BadRequest(grape);
    }

    /// <summary>
    /// Returns a list of Grapes
    /// </summary>
    /// <response code="200">Returns a list of grapes</response>
    /// <response code="204">If the grapes list is empty</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllGrapesAsync()
    {
        var grapes = await _mediator.Send(new GetGrapesQuery());
        return grapes.Data != null && grapes.Data.Any()
            ? Ok(grapes)
            : NoContent();
    }

    /// <summary>
    /// Search and return a single Grape
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A single Grape</returns>
    /// <response code="200">Found a grape</response>
    /// <response code="404">Searched grape not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGrapeByIdAsync(int id)
    {
        var grape = await _mediator.Send(new GetGrapeByIdQuery(id));
        return grape.Data != null
            ? Ok(grape)
            : NotFound(grape);
    }

    /// <summary>
    /// Search a Grape and updates it's content
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedGrape"></param>
    /// <returns>The newly updated Grape</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH /api/Grapes
    ///     {
    ///        "name": "Shiraz"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Successfully updated a grape</response>
    /// <response code="400">Grape to be updated not found</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGrapeAsync(int id, GrapeInput updatedGrape)
    {
        var validationResult = await _grapeValidator.ValidateAsync(updatedGrape);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(", ", validationResult.Errors));

        var grape = await _mediator.Send(new UpdateGrapeCommand(id, updatedGrape));
        return grape.Data != null
            ? Ok(grape)
            : BadRequest(grape);
    }

    /// <summary>
    /// Removes a Grape
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Successfully removed a grape</response>
    /// <response code="400">Grape to be remove not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveGrapeAsync(int id)
    {
        var grape = await _mediator.Send(new RemoveGrapeCommand(id));
        return grape.Success
            ? NoContent()
            : NotFound(grape);
    }
}
