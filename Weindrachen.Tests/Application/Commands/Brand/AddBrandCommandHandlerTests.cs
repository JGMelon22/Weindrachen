using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.Application.Handlers.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Commands.Brand;

public class AddBrandCommandHandlerTests
{
    private readonly IBrandRepository _brandRepository;

    public AddBrandCommandHandlerTests()
    {
        _brandRepository = A.Fake<IBrandRepository>();
    }

    [Fact]
    public async Task AddBrandCommandHandler_Handle_ReturnsBrandResult()
    {
        // Arrange
        var newBrand = new BrandInput("Gato Negro", Country.Chile);
        var handler = new AddBrandCommandHandler(_brandRepository);
        var brandResult = new BrandResult
        {
            Id = 1,
            Name = "San Pedro",
            Country = Country.Chile
        };
        var serviceResponse = new ServiceResponse<BrandResult>
        {
            Data = brandResult
        };

        A.CallTo(() => _brandRepository.AddNewBrandAsync(newBrand))
            .Returns(Task.FromResult(serviceResponse));

        var command = new AddBrandCommand(newBrand);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandResult);
        A.CallTo(() => _brandRepository.AddNewBrandAsync(newBrand))
            .MustHaveHappenedOnceExactly();
    }
}