using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weindrachen.DTOs.Grape;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Models;
using Weindrachen.Models.Enums;
using Xunit;

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
            for (int i = 0; i < 10; i++)
            {
                _dbContext.Grapes.Add(new Grape
                {
                    Name = "New Fake Grape"
                });
            }

            _dbContext.SaveChanges();
        }
    }

    [Fact]
    public void GrapeRepository_AddGrape_ReturnsGrape()
    {
        var grapeInput = new GrapeInput("Malbec");
        var result = _repository.AddNewGrapeAsync(grapeInput);

        result.Should().NotBeNull();
    }

    [Fact]
    public void GrapeRepository_GetAllGrapes_ReturnsGrapes()
    {
        var result = _repository.GetAllGrapesAsync();

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<IEnumerable<GrapeResult>>>>();
    }

    [Fact]
    public void GrapeRepository_GetGrapeById_ReturnsGrape()
    {
        int id = 1;

        var result = _repository.GetGrapeByIdAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<GrapeResult>>>();
    }

    [Fact]
    public void GrapeRepository_UpdateGrape_ReturnsGrapes()
    {
        int id = 4;
        var updatedGrape = new GrapeInput("Cabernet Sauvignon");

        var result = _repository.UpdateGrapeAsync(id, updatedGrape);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<GrapeResult>>>();
    }

    [Fact]
    public void GrapeRepository_RemoveGrape_ReturnsSuccess()
    {
        int id = 3;

        var result = _repository.RemoveGrapeAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<bool>>>();
    }
}