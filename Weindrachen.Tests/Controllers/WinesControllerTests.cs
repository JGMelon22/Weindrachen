using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.Application.Queries.BrandGrapeWine;
using Weindrachen.Application.Queries.Wine;
using Weindrachen.Controllers;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.DTOs.GrapeWine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Controllers;

public class WinesControllerTests
{
    private readonly WinesController _controller;
    private readonly IMediator _mediator;
    private readonly IValidator<WineInput> _validator;

    public WinesControllerTests()
    {
        _mediator = A.Fake<IMediator>();
        _validator = A.Fake<IValidator<WineInput>>();

        // SUT
        _controller = new WinesController(_mediator, _validator);
    }

    [Fact]
    public async Task WinesController_AddWineAsync_ReturnsWine()
    {
        // Arrange
        var newWine = new WineInput("Passo Los Valles",
            27.0M,
            true,
            13.0F,
            Country.Chile,
            1,
            new List<GrapeWineInput>
            {
                new(1)
            },
            Taste.Plum
        );
        var validationResult = new ValidationResult();
        var wineResult = new WineResult
        {
            Id = 1,
            Name = "Passo Los Valles",
            Price = 27.0M,
            IsDoc = true,
            AlcoholicLevel = 13.0F,
            Country = Country.Chile,
            Taste = Taste.Plum
        };

        var serviceResponse = new ServiceResponse<WineResult> { Data = wineResult };

        A.CallTo(() => _validator.ValidateAsync(newWine, default))
            .Returns(Task.FromResult(validationResult));

        A.CallTo(() => _mediator.Send(A<AddWineCommand>.That.Matches(x => x.NewWine == newWine), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.AddWineAsync(newWine);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(wineResult);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task WinesController_GetAllWinesAsync_ReturnsWines()
    {
        // Arrange
        var winesResult = new List<WineResult>
        {
            new()
            {
                Id = 1,
                Name = "Passo Los Valles",
                Price = 27.0M,
                IsDoc = true,
                AlcoholicLevel = 13.0F,
                Country = Country.Chile,
                Taste = Taste.Plum
            },

            new()
            {
                Id = 2,
                Name = "Chateau Margaux",
                Price = 450.0M,
                IsDoc = true,
                AlcoholicLevel = 13.5F,
                Country = Country.France,
                Taste = Taste.Blackberry
            },
            new()
            {
                Id = 3,
                Name = "Barolo Monfortino",
                Price = 350.0M,
                IsDoc = true,
                AlcoholicLevel = 14.0F,
                Country = Country.Italy,
                Taste = Taste.Cherry
            }
        };

        var serviceResponse = new ServiceResponse<IEnumerable<WineResult>>
        {
            Data = winesResult
        };

        A.CallTo(() => _mediator.Send(A<GetWinesQuery>._, default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.GetAllWinesAsync();

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data!.Count().Should().Be(3);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task WinesController_GetWineByIdAsync_ReturnsWine()
    {
        // Arrange
        var id = 1;
        var wineResult = new WineResult
        {
            Id = id,
            Name = "Barolo Monfortino",
            Price = 350.0M,
            IsDoc = true,
            AlcoholicLevel = 14.0F,
            Country = Country.Italy,
            Taste = Taste.Cherry
        };

        var serviceResponse = new ServiceResponse<WineResult>
        {
            Data = wineResult
        };

        A.CallTo(() => _mediator.Send(A<GetWineByIdQuery>.That.Matches(x => x.Id == id), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.GetWineByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(wineResult);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task WinesController_GetAllWinesInformationAsync_ReturnsWinesAllInfo()
    {
        // Arrange
        var brandsGrapesWinesResult = new List<BrandGrapeWineResult>
        {
            new()
            {
                WineId = 1,
                WineName = "Chateau Margaux",
                Price = 150.00m,
                IsDoc = true,
                AlcoholicLevel = 13.5f,
                Country = Country.France,
                Taste = Taste.Cherry,
                BrandName = "Margaux",
                GrapeName = "Cabernet Sauvignon"
            },
            new()
            {
                WineId = 2,
                WineName = "Barolo",
                Price = 80.00m,
                IsDoc = true,
                AlcoholicLevel = 14.0f,
                Country = Country.Italy,
                Taste = Taste.Cherry,
                BrandName = "Gaja",
                GrapeName = "Nebbiolo"
            },
            new()
            {
                WineId = 3,
                WineName = "Penfolds Grange",
                Price = 350.00m,
                IsDoc = false,
                AlcoholicLevel = 14.5f,
                Country = Country.Spain,
                Taste = Taste.CitricFruits,
                BrandName = "Penfolds",
                GrapeName = "Shiraz"
            }
        };

        var serviceResponse = new ServiceResponse<IEnumerable<BrandGrapeWineResult>> { Data = brandsGrapesWinesResult };

        A.CallTo(() => _mediator.Send(A<GetAllWineInfoQuery>._, default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.GetAllWinesInformationAsync();

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Count().Should().Be(3);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task WinesController_XPTO_ReturnsWineAllInfo()
    {
        // Arrange
        var id = 1;
        var brandGrapeWineResult = new BrandGrapeWineResult
        {
            WineId = id,
            WineName = "Chateau Margaux",
            Price = 150.00m,
            IsDoc = true,
            AlcoholicLevel = 13.5f,
            Country = Country.France,
            Taste = Taste.Cherry,
            BrandName = "Margaux",
            GrapeName = "Cabernet Sauvignon"
        };

        var serviceResponse = new ServiceResponse<BrandGrapeWineResult> { Data = brandGrapeWineResult };

        A.CallTo(() => _mediator.Send(A<GetAllWineInfoByIdQuery>.That.Matches(x => x.Id == id), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.GetAllWinesInformationByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(brandGrapeWineResult);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task WinesController_UpdateWineAsync_ReturnsWine()
    {
        // Arrange
        var id = 1;
        var updatedWine = new WineInput("Passo Los Valles",
            27.0M,
            true,
            13.0F,
            Country.Chile,
            1,
            new List<GrapeWineInput>
            {
                new(1)
            },
            Taste.Plum
        );
        var validationResult = new ValidationResult();

        var wineResult = new WineResult
        {
            Id = 1,
            Name = "Passo Los Valles",
            Price = 27.0M,
            IsDoc = true,
            AlcoholicLevel = 13.0F,
            Country = Country.Chile,
            Taste = Taste.Plum
        };

        var serviceResponse = new ServiceResponse<WineResult> { Data = wineResult };

        A.CallTo(() => _validator.ValidateAsync(updatedWine, default))
            .Returns(Task.FromResult(validationResult));

        A.CallTo(() =>
                _mediator.Send(A<UpdateWineCommand>.That.Matches(x => x.Id == id && x.UpdatedWine == updatedWine),
                    default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.UpdateWineAsync(id, updatedWine);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<OkObjectResult>(result);
        serviceResponse.Data.Should().Be(wineResult);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task WinesController_RemoveWineAsync_ReturnsWine()
    {
        // Arrange
        var id = 1;
        var serviceResponse = new ServiceResponse<bool>();

        A.CallTo(() => _mediator.Send(A<RemoveWineCommand>.That.Matches(x => x.Id == id), default))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.RemoveWineAsync(id);

        // Assert
        result.Should().NotBeNull();
        Assert.IsType<NoContentResult>(result);
        serviceResponse.Success.Should().BeTrue();
        result.Should().BeOfType<NoContentResult>();
    }
}