using MediatR;
using Weindrachen.Application.Queries.Wine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Wine;

public class GetWineQueryHandler : IRequestHandler<GetWinesQuery, ServiceResponse<IEnumerable<WineResult>>>
{
    private readonly IWineRepository _wineRepository;

    public GetWineQueryHandler(IWineRepository wineRepository)
    {
        _wineRepository = wineRepository;
    }

    public async Task<ServiceResponse<IEnumerable<WineResult>>> Handle(GetWinesQuery request,
        CancellationToken cancellationToken)
    {
        return await _wineRepository.GetAllWinesAsync();
    }
}