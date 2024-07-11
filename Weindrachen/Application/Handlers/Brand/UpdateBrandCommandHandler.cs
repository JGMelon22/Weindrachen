using MediatR;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Brand;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, ServiceResponse<BrandResult>>
{
    private readonly IBrandRepository _brandRepository;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<ServiceResponse<BrandResult>> Handle(UpdateBrandCommand request,
        CancellationToken cancellationToken)
    {
        return await _brandRepository.UpdateBrandAsync(request.Id, request.UpdatedBrand);
    }
}