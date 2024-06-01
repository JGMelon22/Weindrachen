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

    [HttpPost]
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

    [HttpGet]
    public async Task<IActionResult> GetAlGrapesAsync()
    {
        var grapes = await _mediator.Send(new GetGrapesQuery());
        return grapes.Data != null
            ? Ok(grapes)
            : NoContent();
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetGrapeByIdAsync(int id)
    {
        var grape = await _mediator.Send(new GetGrapeByIdQuery(id));
        return grape.Data != null
            ? Ok(grape)
            : NotFound(grape);
    }


    [HttpPatch("{id}")]
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveGrapeAsync(int id)
    {
        var grape = await _mediator.Send(new RemoveGrapeCommand(id));
        return grape.Success
            ? NoContent()
            : NotFound(grape);
    }
}