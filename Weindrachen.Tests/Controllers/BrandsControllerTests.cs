using System.Collections;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.Application.Queries.Brand;
using Weindrachen.Controllers;
using Weindrachen.DTOs.Brand;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Controllers;

public class BrandsControllerTests
{
    private readonly IMediator _mediator;
    private readonly IValidator<BrandInput> _validator;
    private readonly BrandsController _controller;

    public BrandsControllerTests()
    {
        _mediator = A.Fake<IMediator>();
        _validator = A.Fake<IValidator<BrandInput>>();

        // SUT
        _controller = new BrandsController(_mediator, _validator);
    }

    [Fact]
    public async Task BrandsController_AddBrandAsync_ReturnsBrand()
    {
        // Arrange
        var newBrand = new BrandInput("Concha y Toro", Country.Chile);
        var validationResult = new ValidationResult();
        var brandResult = new BrandResult
        {
            Id = 1,
            Name = "Concha y Toro",
            Country = Country.Chile
        };

        var serviceResponse = new ServiceResponse<BrandResult> { Data = brandResult };

        A.CallTo(() => _validator.ValidateAsync(newBrand, default))
            .Returns(Task.FromResult(validationResult));

        A.CallTo(() => _mediator.Send(A<AddBrandCommand>.That.Matches(x => x.NewBrand == newBrand), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.AddBrandAsync(newBrand);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(brandResult);
    }

    [Fact]
    public async Task BrandsController_GetAllBrandsAsync_ReturnsBrands()
    {
        // Arrange
        var brandResult = new List<BrandResult>
        {
            new() { Name = "Concha y Toro", Country = Country.Chile },
            new() { Name = "Generic Argentina Wine", Country = Country.Argentina },
            new() { Name = "Generic Brazilian Wine", Country = Country.Brazil }
        };

        var serviceResponse = new ServiceResponse<IEnumerable<BrandResult>>
        {
            Data = brandResult
        };

        A.CallTo(() => _mediator.Send(A<GetBrandsQuery>._, default)).Returns(serviceResponse);

        // Act
        var result = await _controller.GetAllBrandsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task BrandsController_GetBrandByIdAsync_ReturnsBrand()
    {
        // Arrange
        int id = 1;

        var brandResult = new BrandResult()
        {
            Name = "Concha y Toro", Country = Country.Chile
        };

        var serviceResponse = new ServiceResponse<BrandResult>
        {
            Data = brandResult
        };

        A.CallTo(() => _mediator.Send(A<GetBrandByIdQuery>.That.Matches(x => x.Id == id), default))
            .Returns(serviceResponse);

        // Act
        var result = await _controller.GetBrandByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task BrandsController_UpdateBrandAsync_ReturnsBrand()
    {
        // Arrange
        int id = 1;
        var updatedBrand = new BrandInput("Gato Negro", Country.Uruguay);
        var validationResult = new ValidationResult();
        var brandResult = new BrandResult
        {
            Id = 1,
            Name = "Gato Negro",
            Country = Country.Chile
        };

        var serviceResponse = new ServiceResponse<BrandResult> { Data = brandResult };

        A.CallTo(() => _validator.ValidateAsync(updatedBrand, default))
            .Returns(Task.FromResult(validationResult));

        A.CallTo(() => _mediator.Send(A<UpdateBrandCommand>.That.Matches(x =>x.Id == id && x.UpdatedBrand == updatedBrand), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.UpdateBrandAsync(id, updatedBrand);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(brandResult);
    }

    [Fact]
    public async Task BrandsController_AddBrandAsync_ReturnsSuccess()
    {
        // Arrange
        int id = 1;
        var serviceResponse = new ServiceResponse<bool>();

        A.CallTo(() => _mediator.Send(A<RemoveBrandCommand>.That.Matches(x => x.Id == id), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.RemoveBrandAsync(id);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<NoContentResult>(result);
        serviceResponse.Success.Should().Be(true);
        serviceResponse.Should().BeOfType<ServiceResponse<bool>>();
    }
}