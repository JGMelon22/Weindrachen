using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weindrachen.DTOs.GrapeWine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Repository;

public class WineRepositoryTests
{
    private readonly AppDbContext _dbContext;
    private readonly WineRepository _repository;

    public WineRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        _repository = new WineRepository(_dbContext);

        if (_dbContext.Grapes.Count() == 0)
            for (var i = 0; i < 10; i++)
            {
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
    }

    [Fact]
    public async Task WineRepository_AddWine_ReturnsWine()
    {
        // Arrange
        var wineInput = new WineInput(
            "Passo Los Vales",
            29.00M,
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
        var wineResult = new WineResult
        {
            Id = 11,
            Name = "Passo Los Vales",
            Price = 29.00M,
            IsDoc = true,
            AlcoholicLevel = 13.0F,
            Country = Country.Chile,
            Taste = Taste.Plum
        };

        // Act
        var result = await _repository.AddNewWineAsync(wineInput);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServiceResponse<WineResult>>();
    }

    [Fact]
    public async Task WineRepository_GetAllWines_ReturnsWines()
    {
        // Act
        var result = await _repository.GetAllWinesAsync();

        // Assert
        result.Should().NotBeNull();
        result.Data!.Count().Should().Be(10);
        result.Should().BeOfType<ServiceResponse<IEnumerable<WineResult>>>();
    }

    [Fact]
    public async Task WineRepository_GetWineById_ReturnsWine()
    {
        // Arrange
        var id = 1;
        var wineResult = new WineResult
        {
            Id = id,
            Name = "New Fake Wine - 0#",
            Price = 25.00M,
            IsDoc = true,
            AlcoholicLevel = 14.0F,
            Country = Country.Brazil,
            Taste = Taste.Blackberry
        };

        // Act
        var result = await _repository.GetWineByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(wineResult);
        result.Should().BeOfType<ServiceResponse<WineResult>>();
    }

    [Fact]
    public async Task WineRepository_UpdateWine_ReturnsWine()
    {
        // Arrange
        var id = 3;
        var updatedWine = new WineInput(
            "Casillero del Diablo",
            35.00M,
            true,
            15.0F,
            Country.Chile,
            1,
            new List<GrapeWineInput>
            {
                new(2)
            },
            Taste.Cherry
        );
        var wineResult = new WineResult
        {
            Id = id,
            Name = "Casillero del Diablo",
            Price = 35.00M,
            IsDoc = true,
            AlcoholicLevel = 15.0F,
            Country = Country.Chile,
            Taste = Taste.Cherry
        };

        // Act
        var result = await _repository.UpdateWineAsync(id, updatedWine);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(wineResult);
        result.Should().BeOfType<ServiceResponse<WineResult>>();
    }

    [Fact]
    public async Task WineRepository_RemoveWine_ReturnsSuccess()
    {
        // Arrange
        var id = 1;

        // Act
        var result = await _repository.RemoveWineAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Should().BeOfType<ServiceResponse<bool>>();
    }
}