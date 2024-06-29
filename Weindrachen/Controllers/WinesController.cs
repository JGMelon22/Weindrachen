using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.Application.Queries.BrandGrapeWine;
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

    /// <summary>
    /// Includes a new Wine
    /// </summary>
    /// <param name="newWine"></param>
    /// <returns>A newly created Wine</returns>
    /// <remarks>
    /// Sample request:
    ///     POST /api/Wines
    ///     {
    ///         "name": "Paso los Valles",
    ///         "price": 20.00,
    ///         "isDoc": true,
    ///         "alcoholicLevel": 13.0,
    ///         "country": "Chile",
    ///         "brandId": 1,
    ///         "grapeWines": [
    ///             {
    ///                 "grapeId": 1
    ///             }
    ///         ],
    ///         "taste": "Plum"
    ///     }
    /// </remarks>
    /// <response code="200">Returns the newly created wine</response>
    /// <response code="400">If wine is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Returns a list of Wines
    /// </summary>
    /// <response code="200">Returns a list of wines</response>
    /// <response code="204">If the wines list is empty</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllWinesAsync()
    {
        var wines = await _mediator.Send(new GetWinesQuery());
        return wines.Data != null
            ? Ok(wines)
            : NoContent();
    }

    /// <summary>
    /// Search and return a single Wine
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A single Wine</returns>
    /// <response code="200">Found a wine</response>
    /// <response code="404">Searched wine not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWineByIdAsync(int id)
    {
        var wine = await _mediator.Send(new GetWineByIdQuery(id));
        return wine.Data != null
            ? Ok(wine)
            : NotFound(wine);
    }

    /// <summary>
    /// Returns a list of Wine Full Info
    /// </summary>
    /// <response code="200">Returns a list of wine with full info</response>
    /// <response code="204">If the wine with full info list is empty</response>
    [HttpGet("all-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllWinesInformationAsync()
    {
        var allWinesInfo = await _mediator.Send(new GetAllWineInfoQuery());
        return allWinesInfo.Data != null
            ? Ok(allWinesInfo)
            : NoContent();
    }

    /// <summary>
    /// Search and return a single Wine Full Info
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A single Wine Full Info</returns>
    /// <response code="200">Found a wine with full info</response>
    /// <response code="404">Searched wine full info not found</response>
    [HttpGet("all-information/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllWinesInformationByIdAsync(int id)
    {
        var allWinesInfo = await _mediator.Send(new GetAllWineInfoByIdQuery(id));
        return allWinesInfo.Data != null
            ? Ok(allWinesInfo)
            : NotFound(allWinesInfo);
    }

    /// <summary>
    /// Search a wine and updates its content
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedWine"></param>
    /// <returns>The newly updated Wine</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/Wines
    ///     {
    ///         "name": "Casillero del Diablo",
    ///         "price": 35.00,
    ///         "isDoc": true,
    ///         "alcoholicLevel": 14.0,
    ///         "country": "Chile",
    ///         "brandId": 1,
    ///         "grapeWines": [
    ///             {
    ///                 "grapeId": 1
    ///             }
    ///         ],
    ///         "taste": "Cherry"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Successfully updated a wine</response>
    /// <response code="400">Wine to be updated not found</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Removes a Wine
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Successfully removed a wine</response>
    /// <response code="400">Wine to be remove not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveWineAsync(int id)
    {
        var wine = await _mediator.Send(new RemoveWineCommand(id));
        return wine.Success
            ? NoContent()
            : BadRequest(wine);
    }
}