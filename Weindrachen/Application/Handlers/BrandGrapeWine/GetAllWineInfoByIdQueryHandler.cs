using MediatR;
using Weindrachen.Application.Queries.BrandGrapeWine;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.BrandGrapeWine;

public class
    GetAllWineInfoByIdHandler : IRequestHandler<GetAllWineInfoByIdQuery, ServiceResponse<BrandGrapeWineResult>>
{
    private readonly IBrandGrapeWineRepository _brandGrapeWineRepository;

    public GetAllWineInfoByIdHandler(IBrandGrapeWineRepository brandGrapeWineRepository)
    {
        _brandGrapeWineRepository = brandGrapeWineRepository;
    }

    public async Task<ServiceResponse<BrandGrapeWineResult>> Handle(GetAllWineInfoByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _brandGrapeWineRepository.GetWineInformationByIdAsync(request.Id);
    }
}