using MediatR;
using Weindrachen.Application.Queries.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Brand;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, ServiceResponse<BrandResult>>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<ServiceResponse<BrandResult>> Handle(GetBrandByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _brandRepository.GetBrandByIdAsync(request.Id);
    }
}