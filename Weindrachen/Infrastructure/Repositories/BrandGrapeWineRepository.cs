using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Infrastructure.Data;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Repositories;

public class BrandGrapeWineRepository : IBrandGrapeWineRepository
{
    private readonly AppDbContext _dbContext;

    public BrandGrapeWineRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<IEnumerable<BrandGrapeWineResult>>> GetAllWinesInformationAsync()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<BrandGrapeWineResult>>();

        try
        {
            var brandGrapeWines = await (from g in _dbContext.Grapes
                                         join gw in _dbContext.GrapesWines on g.Id equals gw.GrapeId
                                         join w in _dbContext.Wines on gw.WineId equals w.Id
                                         join b in _dbContext.Brands on w.BrandId equals b.Id
                                         select new BrandGrapeWineResult
                                         {
                                             WineId = w.Id,
                                             WineName = w.Name,
                                             Price = w.Price,
                                             IsDoc = w.IsDoc,
                                             Country = w.Country,
                                             AlcoholicLevel = w.AlcoholicLevel,
                                             Taste = w.Taste,
                                             // BrandId = b.Id,
                                             BrandName = b.Name,
                                             // GrapeId = g.Id,
                                             GrapeName = g.Name
                                         }).ToListAsync();

            serviceResponse.Data = brandGrapeWines;
        }

        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}