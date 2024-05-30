using MediatR;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.DTOs.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Brand;

public class AddBrandHandler : IRequestHandler<AddBrandCommand, ServiceResponse<BrandResult>>
{
    private readonly IBrandRepository _brandRepository;
    
    public AddBrandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<ServiceResponse<BrandResult>> Handle(AddBrandCommand request, CancellationToken cancellationToken)
    {
        return await _brandRepository.AddNewBrandAsync(request.NewBrand);
    }
}