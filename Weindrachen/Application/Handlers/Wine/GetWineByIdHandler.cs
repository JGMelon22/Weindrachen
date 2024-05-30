using MediatR;
using Weindrachen.Application.Queries.Wine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Wine;

public class GetWineByIdHandler : IRequestHandler<GetWineByIdQuery, ServiceResponse<WineResult>>
{
    private readonly IWineRepository _wineRepository;

    public GetWineByIdHandler(IWineRepository wineRepository)
    {
        _wineRepository = wineRepository;
    }

    public async Task<ServiceResponse<WineResult>> Handle(GetWineByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _wineRepository.GetWineByIdAsync(request.Id);
    }
}