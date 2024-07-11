using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.Brand;
using Weindrachen.Application.Queries.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Queries.Brand;

public class GetBrandByIdQueryHandlerTests
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandByIdQueryHandlerTests()
    {
        _brandRepository = A.Fake<IBrandRepository>();
    }

    [Fact]
    public async Task GetBrandByIdQueryHandler_Handle_ReturnsBrandsResult()
    {
        // Arrange
        var brandId = 1;
        var handler = new GetBrandByIdQueryHandler(_brandRepository);
        var brandResult = new BrandResult
        {
            Id = brandId,
            Name = "Concha y Toro",
            Country = Country.Argentina
        };
        var serviceResponse = new ServiceResponse<BrandResult>
        {
            Data = brandResult
        };

        A.CallTo(() => _brandRepository.GetBrandByIdAsync(brandId)).Returns(Task.FromResult(serviceResponse));

        var query = new GetBrandByIdQuery(brandId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandResult);
        A.CallTo(() => _brandRepository.GetBrandByIdAsync(brandId))
            .MustHaveHappenedOnceExactly();
    }
}