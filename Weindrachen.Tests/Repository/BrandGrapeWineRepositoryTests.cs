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

        if (_dbContext.Brands.Count() == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                _dbContext.Brands.Add(new Brand
                {
                    Name = "New Fake Brand",
                    Country = Country.Argentina
                });
            }

            _dbContext.SaveChanges();
        }

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

        Random random = new Random();
        int item = random.Next(19, 9999);
        decimal randomDecimal = Convert.ToDecimal(item);

        if (_dbContext.Grapes.Count() == 0)
        {
            for (int i = 0; i < 10; i++)
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
    }

    [Fact]
    public void BrandGrapeWineRepository_GetAllWinesInformationAsync_ReturnsAllWinesInfo()
    {
        var result = _repository.GetAllWinesInformationAsync();

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<IEnumerable<BrandGrapeWineResult>>>>();
    }

    [Fact]
    public void BrandGrapeWineRepository_GetWinesInformationByIdAsync_ReturnsWineInfo()
    {
        int id = 1;

        var result = _repository.GetWineInformationByIdAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<BrandGrapeWineResult>>>();
    }
}