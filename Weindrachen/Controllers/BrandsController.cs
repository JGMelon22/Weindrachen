using FluentValidation;
using FluentValidation.Results;
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
    private readonly IMediator _mediator;
    private readonly IValidator<BrandInput> _brandValidator;

    public BrandsController(IMediator mediator, IValidator<BrandInput> brandValidator)
    {
        _mediator = mediator;
        _brandValidator = brandValidator;
    }

    [HttpPost]
    public async Task<IActionResult> AddBrandAsync(BrandInput newBrand)
    {
        ValidationResult validationResult = await _brandValidator.ValidateAsync(newBrand);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        var brand = await _mediator.Send(new AddBrandCommand(newBrand));
        return brand.Data != null
            ? Ok(brand)
            : BadRequest(brand);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrandsAsync()
    {
        var brands = await _mediator.Send(new GetBrandsQuery());
        return brands.Data != null
            ? Ok(brands)
            : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandByIdAsync(int id)
    {
        var brand = await _mediator.Send(new GetBrandByIdQuery(id));
        return brand.Data != null
            ? Ok(brand)
            : NotFound(brand);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateBrandAsync(int id, BrandInput updatedBrand)
    {
        ValidationResult validationResult = await _brandValidator.ValidateAsync(updatedBrand);
        if (!validationResult.IsValid)
            return BadRequest(string.Join(',', validationResult.Errors));

        var brand = await _mediator.Send(new UpdateBrandCommand(id, updatedBrand));
        return brand.Data != null
            ? Ok(brand)
            : BadRequest(brand);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveBrandAsync(int id)
    {
        var brand = await _mediator.Send(new RemoveBrandCommand(id));
        return brand.Success != false
            ? NoContent()
            : BadRequest(brand);
    }
}