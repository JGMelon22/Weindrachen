using MediatR;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Grape;

public class AddGrapeHandler : IRequestHandler<AddGrapeCommand, ServiceResponse<GrapeResult>>
{
    private readonly IGrapeRepository _grapeRepository;

    public AddGrapeHandler(IGrapeRepository grapeRepository)
    {
        _grapeRepository = grapeRepository;
    }
    
    public async Task<ServiceResponse<GrapeResult>> Handle(AddGrapeCommand request, CancellationToken cancellationToken)
    {
        return await _grapeRepository.AddNewGrapeAsync(request.NewGrape);
    }
}