using MediatR;
using Weindrachen.Application.Queries.BrandGrapeWine;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.BrandGrapeWine;

public class
    GetAllWineInfoQueryHandler : IRequestHandler<GetAllWineInfoQuery,
    ServiceResponse<IEnumerable<BrandGrapeWineResult>>>
{
    private readonly IBrandGrapeWineRepository _brandGrapeWineRepository;

    public GetAllWineInfoQueryHandler(IBrandGrapeWineRepository brandGrapeWineRepository)
    {
        _brandGrapeWineRepository = brandGrapeWineRepository;
    }

    public async Task<ServiceResponse<IEnumerable<BrandGrapeWineResult>>> Handle(GetAllWineInfoQuery request,
        CancellationToken cancellationToken)
    {
        return await _brandGrapeWineRepository.GetAllWinesInformationAsync();
    }
}