using MediatR;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Grape;

public class UpdateGrapeHandler : IRequestHandler<UpdateGrapeCommand, ServiceResponse<GrapeResult>>
{
    private readonly IGrapeRepository _grapeRepository;

    public UpdateGrapeHandler(IGrapeRepository grapeRepository)
    {
        _grapeRepository = grapeRepository;
    }

    public async Task<ServiceResponse<GrapeResult>> Handle(UpdateGrapeCommand request,
        CancellationToken cancellationToken)
    {
        return await _grapeRepository.UpdateGrapeAsync(request.Id, request.UpdatedGrape);
    }
}