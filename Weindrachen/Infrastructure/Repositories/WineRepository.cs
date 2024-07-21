using Weindrachen.DTOs.Wine;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Mappling;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Repositories;

public class WineRepository : IWineRepository
{
    private readonly AppDbContext _dbContext;

    public WineRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<WineResult>> AddNewWineAsync(WineInput newWine)
    {
        var serviceResponse = new ServiceResponse<WineResult>();

        try
        {
            var wine = WineMapper.WineToWineInput(newWine);

            await _dbContext.AddAsync(wine);
            await _dbContext.SaveChangesAsync();

            var wineResult = WineMapper.WineToWineResult(wine);

            serviceResponse.Data = wineResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<WineResult>>> GetAllWinesAsync()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<WineResult>>();

        try
        {
            var wines = await _dbContext.Wines
                            .AsNoTracking()
                            .ToListAsync();

            var wineResult = wines.Select(WineMapper.WineToWineResult);

            serviceResponse.Data = wineResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<WineResult>> GetWineByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<WineResult>();

        try
        {
            var wine = await _dbContext.Wines
                           .FindAsync(id)
                       ?? throw new Exception($"Wine with id {id} not found!");

            var wineResult = WineMapper.WineToWineResult(wine);

            serviceResponse.Data = wineResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<WineResult>> UpdateWineAsync(int id, WineInput updatedWine)
    {
        var serviceResponse = new ServiceResponse<WineResult>();

        try
        {
            var wine = await _dbContext.Wines
                           .FindAsync(id)
                       ?? throw new Exception($"Wine with id {id} not found!");

            WineMapper.ApplyUpdate(updatedWine, wine);

            var wineResult = WineMapper.WineToWineResult(wine);

            serviceResponse.Data = wineResult;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> RemoveWineAsync(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            var wine = await _dbContext.Wines
                           .FindAsync(id)
                       ?? throw new Exception("Wine with id {id} not found!");

            _dbContext.Wines.Remove(wine);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
