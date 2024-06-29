using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.Application.Queries.Brand;
using Weindrachen.DTOs.Brand;

namespace Weindrachen.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IValidator<BrandInput> _brandValidator;
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator, IValidator<BrandInput> brandValidator)
    {
        _mediator = mediator;
        _brandValidator = brandValidator;
    }

    /// <summary>
    /// Includes a new Brand
    /// </summary>
    /// <param name="newBrand"></param>
    /// <returns>A newly created Brand</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/Brands
    ///     {
    ///        "name": "Concha y Toro",
    ///        "country": Chile
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the newly created brand</response>
    /// <response code="400">If brand is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBrandAsync(BrandInput newBrand)
    {
        var validationResult = await _brandValidator.ValidateAsync(newBrand);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        var brand = await _mediator.Send(new AddBrandCommand(newBrand));
        return brand.Data != null
            ? Ok(brand)
            : BadRequest(brand);
    }

    /// <summary>
    /// Returns a list of Brands
    /// </summary>
    /// <response code="200">Returns a list of brands</response>
    /// <response code="204">If the brands list is empty</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBrandsAsync()
    {
        var brands = await _mediator.Send(new GetBrandsQuery());
        return brands.Data != null
            ? Ok(brands)
            : NoContent();
    }

    /// <summary>
    /// Search and return a single Brand
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A single Brand</returns>
    /// <response code="200">Found a brand</response>
    /// <response code="404">Searched brand not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBrandByIdAsync(int id)
    {
        var brand = await _mediator.Send(new GetBrandByIdQuery(id));
        return brand.Data != null
            ? Ok(brand)
            : NotFound(brand);
    }

    /// <summary>
    /// Search a brand and updates it's content
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedBrand"></param>
    /// <returns>The newly updated Brand</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH /api/Brands
    ///     {
    ///        "name": "Vinicola Miolo",
    ///        "country": Brazil
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Successfully updated a brand</response>
    /// <response code="400">Brand to be updated not found</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBrandAsync(int id, BrandInput updatedBrand)
    {
        var validationResult = await _brandValidator.ValidateAsync(updatedBrand);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        var brand = await _mediator.Send(new UpdateBrandCommand(id, updatedBrand));
        return brand.Data != null
            ? Ok(brand)
            : BadRequest(brand);
    }

    /// <summary>
    /// Removes a Brand
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Successfully removed a brand</response>
    /// <response code="400">Brand to be remove not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveBrandAsync(int id)
    {
        var brand = await _mediator.Send(new RemoveBrandCommand(id));
        return brand.Success
            ? NoContent()
            : BadRequest(brand);
    }
}