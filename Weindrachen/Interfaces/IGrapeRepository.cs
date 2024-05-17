using Weindrachen.DTOs.Grape;
using Weindrachen.Models;

namespace Weindrachen.Interfaces;

public interface IGrapeRepository
{
    Task<ServiceResponse<GrapeResult>> AddNewGrapeAsync(GrapeInput newGrape);
    Task<ServiceResponse<IEnumerable<GrapeResult>>> GetAllGrapesAsync();
    Task<ServiceResponse<GrapeResult>> GetGrapeByIdAsync(int id);
    Task<ServiceResponse<GrapeResult>> UpdateGrapeAsync(int id, GrapeInput updatedGrape);
    Task<ServiceResponse<bool>> RemoveGrapeAsync(int id);
}