using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.BrandGrapeWine;
using Weindrachen.Application.Queries.BrandGrapeWine;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Queries.BrandGrapeWine;

public class GetAllWineInfoQueryHandlerTests
{
    private readonly IBrandGrapeWineRepository _brandGrapeWineRepository;

    public GetAllWineInfoQueryHandlerTests()
    {
        _brandGrapeWineRepository = A.Fake<IBrandGrapeWineRepository>();
    }

    [Fact]
    public async Task GetAllWineInfoQueryHandler_Handle_ReturnsBrandGrapeWineResult()
    {
        // Arrange 
        var handler = new GetAllWineInfoHandler(_brandGrapeWineRepository);
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
        var serviceResponse = new ServiceResponse<IEnumerable<BrandGrapeWineResult>>
        {
            Data = brandsGrapesWinesResult
        };

        A.CallTo(() => _brandGrapeWineRepository.GetAllWinesInformationAsync())
            .Returns(Task.FromResult(serviceResponse));

        var query = new GetAllWineInfoQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandsGrapesWinesResult);
        A.CallTo(() => _brandGrapeWineRepository.GetAllWinesInformationAsync())
            .MustHaveHappened();
    }
}