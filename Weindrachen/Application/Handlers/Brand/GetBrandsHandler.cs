using MediatR;
using Weindrachen.Application.Queries.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Brand;

public class GetBrandsHandler : IRequestHandler<GetBrandsQuery, ServiceResponse<IEnumerable<BrandResult>>>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<ServiceResponse<IEnumerable<BrandResult>>> Handle(GetBrandsQuery request,
        CancellationToken cancellationToken)
    {
        return await _brandRepository.GetAllBrandsAsync();
    }
}