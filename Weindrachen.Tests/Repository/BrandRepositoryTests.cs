using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weindrachen.DTOs.Brand;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Repository;

public class BrandRepositoryTests
{
    private readonly AppDbContext _dbContext;
    private readonly BrandRepository _repository;

    public BrandRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        _repository = new BrandRepository(_dbContext);

        if (_dbContext.Brands.Count() == 0)
        {
            for (var i = 0; i < 10; i++)
                _dbContext.Brands.Add(new Brand
                {
                    Name = "New Fake Brand",
                    Country = Country.Argentina
                });

            _dbContext.SaveChanges();
        }
    }

    [Fact]
    public async Task BrandRepository_AddBrand_ReturnsBrand()
    {
        // Arrange
        var newBrand = new BrandInput("Concha y Toro", Country.Chile);
        var brandResult = new BrandResult
        {
            Id = 11,
            Name = "Concha y Toro",
            Country = Country.Chile
        };

        // Act
        var result = await _repository.AddNewBrandAsync(newBrand);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandResult);
        result.Should().BeOfType<ServiceResponse<BrandResult>>();
    }

    [Fact]
    public async Task BrandRepository_GetAllBrands_ReturnsBrands()
    {
        // Act
        var result = await _repository.GetAllBrandsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Data!.Count().Should().Be(10);
        result.Should().BeOfType<ServiceResponse<IEnumerable<BrandResult>>>();
    }

    [Fact]
    public async Task BrandRepository_GetBrandById_ReturnsBrand()
    {
        // Arrange
        var id = 1;
        var brandResult = new BrandResult
        {
            Id = 1,
            Name = "New Fake Brand",
            Country = Country.Argentina
        };

        // Act
        var result = await _repository.GetBrandByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandResult);
        result.Should().BeOfType<ServiceResponse<BrandResult>>();
    }

    [Fact]
    public async Task BrandRepository_UpdateBrand_ReturnsBrands()
    {
        // Arrange
        var id = 4;
        var updatedBrand = new BrandInput("Catena Zapata", Country.Argentina);
        var brandResult = new BrandResult
        {
            Id = 4,
            Name = "Catena Zapata",
            Country = Country.Argentina
        };

        // Act
        var result = await _repository.UpdateBrandAsync(id, updatedBrand);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(brandResult);
        result.Should().BeOfType<ServiceResponse<BrandResult>>();
    }

    [Fact]
    public async Task BrandRepository_RemoveBrand_ReturnsSuccess()
    {
        // Arrange
        var id = 3;

        // Act 
        var result = await _repository.RemoveBrandAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Should().BeOfType<ServiceResponse<bool>>();
    }
}