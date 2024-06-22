using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.Application.Handlers.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Commands.Brand;

public class UpdateBrandCommandHandlerTests
{
    private readonly IBrandRepository _brandRepository;

    public UpdateBrandCommandHandlerTests()
    {
        _brandRepository = A.Fake<IBrandRepository>();
    }

    [Fact]
    public async Task UpdateBrandCommandHandler_Handle_ReturnsBrandResult()
    {
        // Arrange
        var id = 1;
        var updatedBrand = new BrandInput("Santa Carolina", Country.Chile);
        var handler = new UpdateBrandHandler(_brandRepository);
        var brandResult = new BrandResult
        {
            Id = id,
            Name = "Santa Carolina",
            Country = Country.Chile
        };
        var serviceResponse = new ServiceResponse<BrandResult> { Data = brandResult };

        A.CallTo(() => _brandRepository.UpdateBrandAsync(id, updatedBrand))
            .Returns(Task.FromResult(serviceResponse));

        var command = new UpdateBrandCommand(id, updatedBrand);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(brandResult);
        A.CallTo(() => _brandRepository.UpdateBrandAsync(id, updatedBrand))
            .MustHaveHappenedOnceExactly();
    }
}