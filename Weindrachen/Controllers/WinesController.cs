using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.Application.Queries.Wine;
using Weindrachen.DTOs.Wine;

namespace Weindrachen.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WinesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<WineInput> _wineValidator;

    public WinesController(IMediator mediator, IValidator<WineInput> wineValidator)
    {
        _mediator = mediator;
        _wineValidator = wineValidator;
    }

    [HttpPost]
    public async Task<IActionResult> AddWineAsync(WineInput newWine)
    {
        var validationResult = await _wineValidator.ValidateAsync(newWine);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        var wine = await _mediator.Send(new AddWineCommand(newWine));
        return wine.Data != null
            ? Ok(wine)
            : BadRequest(wine);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWinesAsync()
    {
        var wines = await _mediator.Send(new GetWinesQuery());
        return wines.Data != null
            ? Ok(wines)
            : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWineByIdAsync(int id)
    {
        var wine = await _mediator.Send(new GetWineByIdQuery(id));
        return wine.Data != null
            ? Ok(wine)
            : NotFound(wine);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateWineAsync(int id, WineInput updatedWine)
    {
        var validationResult = await _wineValidator.ValidateAsync(updatedWine);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        var wine = await _mediator.Send(new UpdateWineCommand(id, updatedWine));
        return wine.Data != null
            ? Ok(wine)
            : BadRequest(wine);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveWineAsync(int id)
    {
        var wine = await _mediator.Send(new RemoveWineCommand(id));
        return wine.Success
            ? NoContent()
            : BadRequest(wine);
    }
}