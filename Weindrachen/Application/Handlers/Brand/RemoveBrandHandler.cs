using MediatR;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Brand;

public class RemoveBrandHandler : IRequestHandler<RemoveBrandCommand, ServiceResponse<bool>>
{
    private readonly IBrandRepository _brandRepository;

    public RemoveBrandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<ServiceResponse<bool>> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
    {
        return await _brandRepository.RemoveBrandAsync(request.Id);
    }
}