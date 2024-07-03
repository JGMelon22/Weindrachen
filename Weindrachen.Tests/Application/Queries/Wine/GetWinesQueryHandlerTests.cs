using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.Wine;
using Weindrachen.Application.Queries.Wine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Queries.Wine;

public class GetWinesQueryHandlerTests
{
    private readonly IWineRepository _wineRepository;

    public GetWinesQueryHandlerTests()
    {
        _wineRepository = A.Fake<IWineRepository>();
    }

    [Fact]
    public async Task GetWinesQueryHandler_Handle_ReturnsWinesResult()
    {
        // Arrange
        var handler = new GetWineHandler(_wineRepository);
        var winesResult = new List<WineResult>
        {
            new()
            {
                Id = 1, Name = "Da Pipa",
                Price = 19.99M,
                IsDoc = true,
                AlcoholicLevel = 14.0F,
                Country = Country.Portugal,
                Taste = Taste.CitricFruits
            },
            new()
            {
                Id = 1, Name = "Casillero del Diablo Red",
                Price = 50.00M,
                IsDoc = true,
                AlcoholicLevel = 19.0F,
                Country = Country.Chile,
                Taste = Taste.Cherry
            },
            new()
            {
                Id = 1, Name = "Chateau Bel-Air Quinsac Cotes de Bordeaux ",
                Price = 250.35M,
                IsDoc = true,
                AlcoholicLevel = 14.0F,
                Country = Country.France,
                Taste = Taste.Blackberry
            }
        };
        var serviceResponse = new ServiceResponse<IEnumerable<WineResult>>
        {
            Data = winesResult
        };

        A.CallTo(() => _wineRepository.GetAllWinesAsync())
            .Returns(Task.FromResult(serviceResponse));

        var query = new GetWinesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(winesResult);
        A.CallTo(() => _wineRepository.GetAllWinesAsync())
            .MustHaveHappened();
    }
}