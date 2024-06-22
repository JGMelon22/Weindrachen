using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.Application.Queries.Grape;
using Weindrachen.Controllers;
using Weindrachen.DTOs.Grape;
using Weindrachen.Models;

namespace Weindrachen.Tests.Controllers;

public class GrapesControllerTests
{
    private readonly GrapesController _controller;
    private readonly IMediator _mediator;
    private readonly IValidator<GrapeInput> _validator;

    public GrapesControllerTests()
    {
        _mediator = A.Fake<IMediator>();
        _validator = A.Fake<IValidator<GrapeInput>>();

        // SUT
        _controller = new GrapesController(_mediator, _validator);
    }

    [Fact]
    public async Task GrapesController_AddGrapeAsync_ReturnsGrape()
    {
        // Arrange
        var newGrape = new GrapeInput("Bobal");
        var validationResult = new ValidationResult();
        var grapeResult = new GrapeResult
        {
            Id = 1,
            Name = "Bobal"
        };

        var serviceResponse = new ServiceResponse<GrapeResult> { Data = grapeResult };

        A.CallTo(() => _validator.ValidateAsync(newGrape, default))
            .Returns(Task.FromResult(validationResult));

        A.CallTo(() => _mediator.Send(A<AddGrapeCommand>.That.Matches(x => x.NewGrape == newGrape), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.AddGrapeAsync(newGrape);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(grapeResult);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GrapesController_GetAllGrapesAsync_ReturnsGrapes()
    {
        // Arrange
        var grapesResult = new List<GrapeResult>
        {
            new() { Id = 1, Name = "Bobal" },
            new() { Id = 2, Name = "Cabernet Sauvignon" },
            new() { Id = 3, Name = "Syrah" }
        };

        var serviceResponse = new ServiceResponse<IEnumerable<GrapeResult>>
        {
            Data = grapesResult
        };

        A.CallTo(() => _mediator.Send(A<GetGrapesQuery>._, default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.GetAllGrapesAsync();

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Count().Should().Be(3);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GrapesController_GetGrapeByIdAsync_ReturnsGrape()
    {
        // Arrange
        var id = 1;
        var grapeResult = new GrapeResult
        {
            Id = id,
            Name = "Bobal"
        };

        var serviceResponse = new ServiceResponse<GrapeResult>
        {
            Data = grapeResult
        };

        A.CallTo(() => _mediator.Send(A<GetGrapeByIdQuery>.That.Matches(x => x.Id == id), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.GetGrapeByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(grapeResult);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GrapesController_UpdateGrapeAsync_ReturnsGrape()
    {
        // Arrange
        var id = 1;
        var updatedGrape = new GrapeInput("Pinot noir");
        var validationResult = new ValidationResult();
        var grapeResult = new GrapeResult
        {
            Id = id,
            Name = "Pinot noir"
        };

        var serviceResponse = new ServiceResponse<GrapeResult> { Data = grapeResult };

        A.CallTo(() => _validator.ValidateAsync(updatedGrape, default))
            .Returns(validationResult);

        A.CallTo(() =>
            _mediator.Send(A<UpdateGrapeCommand>.That.Matches(x => x.Id == id && x.UpdatedGrape == updatedGrape),
                default)).Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.UpdateGrapeAsync(id, updatedGrape);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(grapeResult);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GrapesController_RemoveGrapeAsync_ReturnsSuccess()
    {
        // Arrange
        var id = 1;
        var serviceResponse = new ServiceResponse<bool>();

        A.CallTo(() => _mediator.Send(A<RemoveGrapeCommand>.That.Matches(x => x.Id == id), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.RemoveGrapeAsync(id);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<NoContentResult>(result);
        serviceResponse.Success.Should().Be(true);
        serviceResponse.Success.Should().BeTrue();
    }
}