using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weindrachen.DTOs.Grape;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Models;

namespace Weindrachen.Tests.Repository;

public class GrapeRepositoryTests
{
    private readonly AppDbContext _dbContext;
    private readonly GrapeRepository _repository;

    public GrapeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        _repository = new GrapeRepository(_dbContext);

        if (_dbContext.Grapes.Count() == 0)
        {
            for (var i = 0; i < 10; i++)
                _dbContext.Grapes.Add(new Grape
                {
                    Name = "New Fake Grape"
                });

            _dbContext.SaveChanges();
        }
    }

    [Fact]
    public async Task GrapeRepository_AddGrape_ReturnsGrape()
    {
        // Arrange
        var newGrape = new GrapeInput("Malbec");
        var grapeResult = new GrapeResult
        {
            Id = 11,
            Name = "Malbec"
        };

        // Act
        var result = await _repository.AddNewGrapeAsync(newGrape);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(grapeResult);
        result.Should().BeOfType<ServiceResponse<GrapeResult>>();
    }

    [Fact]
    public async Task GrapeRepository_GetAllGrapes_ReturnsGrapes()
    {
        // Act
        var result = await _repository.GetAllGrapesAsync();

        // Assert
        result.Should().NotBeNull();
        result.Data!.Count().Should().Be(10);
        result.Should().BeOfType<ServiceResponse<IEnumerable<GrapeResult>>>();
    }

    [Fact]
    public async Task GrapeRepository_GetGrapeById_ReturnsGrape()
    {
        // Arrange
        var id = 1;
        var grapeResult = new GrapeResult
        {
            Id = 1,
            Name = "New Fake Grape"
        };

        // Act
        var result = await _repository.GetGrapeByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(grapeResult);
        result.Should().BeOfType<ServiceResponse<GrapeResult>>();
    }

    [Fact]
    public async Task GrapeRepository_UpdateGrape_ReturnsGrapes()
    {
        // Arrange
        var id = 4;
        var updatedGrape = new GrapeInput("Cabernet Sauvignon");
        var grapeResult = new GrapeResult
        {
            Id = 4,
            Name = "Cabernet Sauvignon"
        };

        // Act
        var result = await _repository.UpdateGrapeAsync(id, updatedGrape);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(grapeResult);
        result.Should().BeOfType<ServiceResponse<GrapeResult>>();
    }

    [Fact]
    public async Task GrapeRepository_RemoveGrape_ReturnsSuccess()
    {
        // Arrange
        var id = 1;

        // Act
        var result = await _repository.RemoveGrapeAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Should().BeOfType<ServiceResponse<bool>>();
    }
}