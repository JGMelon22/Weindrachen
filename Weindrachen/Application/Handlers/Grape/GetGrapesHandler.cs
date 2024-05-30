using MediatR;
using Weindrachen.Application.Queries.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Grape;

public class GetGrapesHandler : IRequestHandler<GetGrapesQuery, ServiceResponse<IEnumerable<GrapeResult>>>
{
    private readonly IGrapeRepository _grapeRepository;

    public GetGrapesHandler(IGrapeRepository grapeRepository)
    {
        _grapeRepository = grapeRepository;
    }

    public async Task<ServiceResponse<IEnumerable<GrapeResult>>> Handle(GetGrapesQuery request,
        CancellationToken cancellationToken)
    {
        return await _grapeRepository.GetAllGrapesAsync();
    }
}