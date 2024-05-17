using Weindrachen.DTOs.Wine;
using Weindrachen.Models;

namespace Weindrachen.Interfaces;

public interface IWineRepository
{
    Task<ServiceResponse<WineResult>> AddNewWineAsync(WineInput newWine);
    Task<ServiceResponse<IEnumerable<WineResult>>> GetAllWinesAsync();
    Task<ServiceResponse<WineResult>> GetWineByIdAsync(int id);
    Task<ServiceResponse<WineResult>> UpdateWineAsync(int id, WineInput updatedWine);
    Task<ServiceResponse<bool>> RemoveWineAsync(int id);
}