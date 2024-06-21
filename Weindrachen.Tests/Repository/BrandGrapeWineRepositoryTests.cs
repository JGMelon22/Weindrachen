using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Repository;

public class BrandGrapeWineRepositoryTests
{
    private readonly AppDbContext _dbContext;
    private readonly BrandGrapeWineRepository _repository;

    public BrandGrapeWineRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        _repository = new BrandGrapeWineRepository(_dbContext);

        if (_dbContext.Grapes.Count() == 0)
        {
            for (var i = 0; i < 10; i++)
                _dbContext.Grapes.Add(new Grape
                {
                    Name = $"New Fake Grape - {i}#"
                });

            _dbContext.SaveChanges();
        }

        if (_dbContext.Brands.Count() == 0)
        {
            for (var i = 0; i < 10; i++)
                _dbContext.Brands.Add(new Brand
                {
                    Name = $"New Fake Brand - {i}#",
                    Country = Country.Argentina
                });

            _dbContext.SaveChanges();
        }

        if (_dbContext.Wines.Count() == 0)
        {
            for (var i = 0; i < 10; i++)
                _dbContext.Wines.Add(new Wine
                {
                    Name = $"New Fake Wine - {i}#",
                    Price = 25.00M,
                    IsDoc = true,
                    AlcoholicLevel = 14.0F,
                    Country = Country.Brazil,
                    BrandId = 1,
                    Taste = Taste.Blackberry
                });

            _dbContext.SaveChanges();
        }

        if (_dbContext.GrapesWines.Count() == 0)
        {
            var grapes = _dbContext.Grapes.ToList();
            var wines = _dbContext.Wines.ToList();

            for (var i = 0; i < 10; i++)
                _dbContext.GrapesWines.Add(new GrapeWine
                {
                    GrapeId = grapes[i].Id,
                    WineId = wines[i].Id
                });

            _dbContext.SaveChanges();
        }
    }

    [Fact]
    public async Task BrandGrapeWineRepository_GetAllWinesInformationAsync_ReturnsAllWinesInfo()
    {
        // Act
        var result = await _repository.GetAllWinesInformationAsync();

        // Assert
        result.Should().NotBeNull();
        result.Data!.Count().Should().Be(10);
        result.Should().BeOfType<ServiceResponse<IEnumerable<BrandGrapeWineResult>>>();
    }

    [Fact]
    public async Task BrandGrapeWineRepository_GetWinesInformationByIdAsync_ReturnsWineInfo()
    {
        // Arrange
        var id = 1;
        var brandGrapeWineResult = new BrandGrapeWineResult
        {
            WineId = id,
            WineName = "New Fake Wine - 0#",
            Price = 25.00M,
            IsDoc = true,
            AlcoholicLevel = 14.0F,
            Country = Country.Brazil,
            Taste = Taste.Blackberry,
            BrandName = "New Fake Brand - 0#",
            GrapeName = "New Fake Grape - 9#"
        };

        // Act
        var result = await _repository.GetWineInformationByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandGrapeWineResult);
        result.Should().BeOfType<ServiceResponse<BrandGrapeWineResult>>();
    }
}