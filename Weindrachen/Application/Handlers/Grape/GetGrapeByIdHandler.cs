using MediatR;
using Weindrachen.Application.Queries.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Grape;

public class GetGrapeByIdHandler : IRequestHandler<GetGrapeByIdQuery, ServiceResponse<GrapeResult>>
{
    private readonly IGrapeRepository _grapeRepository;

    public GetGrapeByIdHandler(IGrapeRepository grapeRepository)
    {
        _grapeRepository = grapeRepository;
    }

    public async Task<ServiceResponse<GrapeResult>> Handle(GetGrapeByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _grapeRepository.GetGrapeByIdAsync(request.Id);
    }
}