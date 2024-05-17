using Weindrachen.DTOs.Brand;
using Weindrachen.Models;

namespace Weindrachen.Interfaces;

public interface IBrandRepository
{
    Task<ServiceResponse<BrandResult>> AddNewBrandAsync(BrandInput newBrand);
    Task<ServiceResponse<IEnumerable<BrandResult>>> GetAllBrandsAsync();
    Task<ServiceResponse<BrandResult>> GetBrandByIdAsync(int id);
    Task<ServiceResponse<BrandResult>> UpdateBrandAsync(int id, BrandInput updatedBrand);
    Task<ServiceResponse<bool>> RemoveBrandAsync(int id);
}