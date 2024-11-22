using Weindrachen.DTOs.Brand;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Mappling;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly AppDbContext _dbContext;

    public BrandRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<BrandResult>> AddNewBrandAsync(BrandInput newBrand)
    {
        var serviceResponse = new ServiceResponse<BrandResult>();

        try
        {
            var brand = BrandMapper.BrandInputToBrand(newBrand);

            await _dbContext.AddAsync(brand);
            await _dbContext.SaveChangesAsync();

            var brandResult = BrandMapper.BrandToBrandResult(brand);

            serviceResponse.Data = brandResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<BrandResult>>> GetAllBrandsAsync()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<BrandResult>>();

        try
        {
            var brands = await _dbContext.Brands
                             .AsNoTracking()
                             .ToListAsync()
                         ?? throw new Exception("Brand list is empty!");

            var brandsResult = brands.Select(BrandMapper.BrandToBrandResult);

            serviceResponse.Data = brandsResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<BrandResult>> GetBrandByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<BrandResult>();

        try
        {
            var brand = await _dbContext.Brands
                            .FindAsync(id)
                        ?? throw new Exception($"Brand with id {id} not found!");

            serviceResponse.Data = BrandMapper.BrandToBrandResult(brand);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> RemoveBrandAsync(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            var brand = await _dbContext.Brands
                            .FindAsync(id)
                        ?? throw new Exception($"Brand with id {id} not found!");

            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<BrandResult>> UpdateBrandAsync(int id, BrandInput updatedBrand)
    {
        var serviceResponse = new ServiceResponse<BrandResult>();

        try
        {
            var brand = await _dbContext.Brands
                            .FindAsync(id)
                        ?? throw new Exception($"Brand with id {id} not found!");

            BrandMapper.ApplyUpdate(updatedBrand, brand);

            await _dbContext.SaveChangesAsync();

            var brandResult = BrandMapper.BrandToBrandResult(brand);

            serviceResponse.Data = brandResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
