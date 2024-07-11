using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.Wine;
using Weindrachen.Application.Queries.Wine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Queries.Wine;

public class GetWineByIdQueryHandlerTests
{
    private readonly IWineRepository _wineRepository;

    public GetWineByIdQueryHandlerTests()
    {
        _wineRepository = A.Fake<IWineRepository>();
    }

    [Fact]
    public async Task GetBrandByIdQueryHandler_Handle_ReturnsWine()
    {
        // Arrange
        var wineId = 1;
        var handler = new GetWineByIdQueryHandler(_wineRepository);
        var wineResult = new WineResult
        {
            Id = wineId,
            Name = "Da Pipa",
            Price = 19.99M,
            IsDoc = true,
            AlcoholicLevel = 14.0F,
            Country = Country.Portugal,
            Taste = Taste.CitricFruits
        };

        var serviceResponse = new ServiceResponse<WineResult>
        {
            Data = wineResult
        };

        A.CallTo(() => _wineRepository.GetWineByIdAsync(wineId))
            .Returns(Task.FromResult(serviceResponse));

        var query = new GetWineByIdQuery(wineId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(wineResult);
        A.CallTo(() => _wineRepository.GetWineByIdAsync(wineId))
            .MustHaveHappenedOnceExactly();
    }
}