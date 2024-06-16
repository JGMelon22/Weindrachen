using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.Brand;
using Weindrachen.Application.Queries.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Queries.Brand;

public class GetBrandsQueryHandlerTests
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandsQueryHandlerTests()
    {
        _brandRepository = A.Fake<IBrandRepository>();
    }

    [Fact]
    public async Task GetBrandsQueryHandler_Handle_ReturnsBrandsResult()
    {
        // Arrange
        var handler = new GetBrandsHandler(_brandRepository);
        var brandsResult = new List<BrandResult>
        {
            new() { Id = 1, Name = "Concha y Toro" },
            new() { Id = 2, Name = "Undurraga" },
            new() { Id = 3, Name = "Casillero del Diablo" }
        };
        var serviceResponse = new ServiceResponse<IEnumerable<BrandResult>>
        {
            Data = brandsResult
        };

        A.CallTo(() => _brandRepository.GetAllBrandsAsync())
            .Returns(Task.FromResult(serviceResponse));

        var query = new GetBrandsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandsResult);
        A.CallTo(() => _brandRepository.GetAllBrandsAsync())
            .MustHaveHappened();
    }
}