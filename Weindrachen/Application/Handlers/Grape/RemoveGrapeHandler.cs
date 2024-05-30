using MediatR;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Grape;

public class RemoveGrapeHandler : IRequestHandler<RemoveGrapeCommand, ServiceResponse<bool>>
{
    private readonly IGrapeRepository _grapeRepository;

    public RemoveGrapeHandler(IGrapeRepository grapeRepository)
    {
        _grapeRepository = grapeRepository;
    }

    public async Task<ServiceResponse<bool>> Handle(RemoveGrapeCommand request, CancellationToken cancellationToken)
    {
        return await _grapeRepository.RemoveGrapeAsync(request.Id);
    }
}