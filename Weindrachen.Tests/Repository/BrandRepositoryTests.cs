using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weindrachen.DTOs.Brand;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Models;
using Weindrachen.Models.Enums;
using Xunit;

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
    }

    [Fact]
    public void BrandRepository_AddBrand_ReturnsBrand()
    {
        var brandInput = new BrandInput("Concha y Toro", Country.Chile);
        var result = _repository.AddNewBrandAsync(brandInput);

        result.Should().NotBeNull();
    }

    [Fact]
    public void BrandRepository_GetAllBrands_ReturnsBrands()
    {
        var result = _repository.GetAllBrandsAsync();

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<IEnumerable<BrandResult>>>>();
    }

    [Fact]
    public void BrandRepository_GetBrandById_ReturnsBrand()
    {
        int id = 1;

        var result = _repository.GetBrandByIdAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<BrandResult>>>();
    }

    [Fact]
    public void BrandRepository_UpdateBrand_ReturnsBrands()
    {
        int id = 4;
        var updatedBrand = new BrandInput("Catena Zapata", Country.Argentina);

        var result = _repository.UpdateBrandAsync(id, updatedBrand);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<BrandResult>>>();
    }

    [Fact]
    public void BrandRepository_RemoveBrand_ReturnsSuccess()
    {
        int id = 3;

        var result = _repository.RemoveBrandAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<bool>>>();
    }
}