using Weindrachen.DTOs.Grape;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Infrastructure.Mappling;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Repositories;

public class GrapeRepository : IGrapeRepository
{
    private readonly AppDbContext _dbContext;

    public GrapeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<GrapeResult>> AddNewGrapeAsync(GrapeInput newGrape)
    {
        var serviceResponse = new ServiceResponse<GrapeResult>();

        try
        {
            var grape = GrapeMapper.GrapeToGrapeInput(newGrape);

            await _dbContext.AddAsync(grape);
            await _dbContext.SaveChangesAsync();

            var grapeResult = GrapeMapper.GrapeToGrapeResult(grape);

            serviceResponse.Data = grapeResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<GrapeResult>>> GetAllGrapesAsync()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<GrapeResult>>();

        try
        {
            var grapes = await _dbContext.Grapes
                             .AsNoTracking()
                             .ToListAsync();

            var grapesResult = grapes.Select(GrapeMapper.GrapeToGrapeResult);

            serviceResponse.Data = grapesResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GrapeResult>> GetGrapeByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<GrapeResult>();

        try
        {
            var grape = await _dbContext.Grapes
                            .FindAsync(id)
                        ?? throw new Exception($"Grape with id {id} not found!");

            var grapeResult = GrapeMapper.GrapeToGrapeResult(grape);

            serviceResponse.Data = grapeResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> RemoveGrapeAsync(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            var grape = await _dbContext.Grapes
                            .FindAsync(id)
                        ?? throw new Exception($"Grape with id {id} not found!");

            _dbContext.Grapes.Remove(grape);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GrapeResult>> UpdateGrapeAsync(int id, GrapeInput updatedGrape)
    {
        var serviceResponse = new ServiceResponse<GrapeResult>();

        try
        {
            var grape = await _dbContext.Grapes
                            .FindAsync(id)
                        ?? throw new Exception($"Grape with id {id} not found!");

            GrapeMapper.ApplyUpdate(updatedGrape, grape);

            var grapeResult = GrapeMapper.GrapeToGrapeResult(grape);

            serviceResponse.Data = grapeResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
