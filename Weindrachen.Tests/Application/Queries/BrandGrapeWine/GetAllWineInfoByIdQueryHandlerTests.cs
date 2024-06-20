using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.BrandGrapeWine;
using Weindrachen.Application.Queries.BrandGrapeWine;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Queries.BrandGrapeWine;

public class GetAllWineInfoByIdQueryHandlerTests
{
    private readonly IBrandGrapeWineRepository _brandGrapeWineRepository;

    public GetAllWineInfoByIdQueryHandlerTests()
    {
        _brandGrapeWineRepository = A.Fake<IBrandGrapeWineRepository>();
    }

    [Fact]
    public async Task GetAllWineInfoByIdQueryHandler_Handle_ReturnsBrandGrapeWineResult()
    {
        // Arrange 
        var wineId = 1;
        var handler = new GetAllWineInfoByIdHandler(_brandGrapeWineRepository);
        var brandGrapeWineResult = new BrandGrapeWineResult
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
        };
        var serviceResponse = new ServiceResponse<BrandGrapeWineResult>
        {
            Data = brandGrapeWineResult
        };

        A.CallTo(() => _brandGrapeWineRepository.GetWineInformationByIdAsync(wineId))
            .Returns(Task.FromResult(serviceResponse));

        var query = new GetAllWineInfoByIdQuery(wineId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandGrapeWineResult);
        A.CallTo(() => _brandGrapeWineRepository.GetWineInformationByIdAsync(wineId))
            .MustHaveHappenedOnceExactly();
    }
}