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

        var random = new Random();
        var item = random.Next(19, 9999);
        var randomDecimal = Convert.ToDecimal(item);

        if (_dbContext.Grapes.Count() == 0)
            for (var i = 0; i < 10; i++)
            {
                _dbContext.Wines.Add(new Wine
                {
                    Name = $"New Fake Wine - {i}#",
                    Price = randomDecimal,
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
    public void WineRepository_AddWine_ReturnsWine()
    {
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

        var result = _repository.AddNewWineAsync(wineInput);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<WineResult>>>();
    }

    [Fact]
    public void WineRepository_GetAllWines_ReturnsWines()
    {
        var result = _repository.GetAllWinesAsync();

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<IEnumerable<WineResult>>>>();
    }

    [Fact]
    public void WineRepository_GetWineById_ReturnsWine()
    {
        var id = 1;
        var result = _repository.GetWineByIdAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<WineResult>>>();
    }

    [Fact]
    public void WineRepository_UpdateWine_ReturnsWine()
    {
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

        var result = _repository.UpdateWineAsync(id, updatedWine);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<WineResult>>>();
    }

    [Fact]
    public void WineRepository_RemoveWine_ReturnsSuccess()
    {
        var id = 1;
        var result = _repository.RemoveWineAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<bool>>>();
    }
}