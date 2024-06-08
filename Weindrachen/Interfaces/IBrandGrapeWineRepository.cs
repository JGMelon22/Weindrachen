using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Models;

namespace Weindrachen.Interfaces;

public interface IBrandGrapeWineRepository
{
    Task<ServiceResponse<IEnumerable<BrandGrapeWineResult>>> GetAllWinesInformationAsync();
}