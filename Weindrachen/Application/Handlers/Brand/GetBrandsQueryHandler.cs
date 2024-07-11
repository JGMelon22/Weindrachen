using MediatR;
using Weindrachen.Application.Queries.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Brand;

public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, ServiceResponse<IEnumerable<BrandResult>>>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<ServiceResponse<IEnumerable<BrandResult>>> Handle(GetBrandsQuery request,
        CancellationToken cancellationToken)
    {
        return await _brandRepository.GetAllBrandsAsync();
    }
}