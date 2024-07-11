using MediatR;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Wine;

public class UpdateWineCommandHandler : IRequestHandler<UpdateWineCommand, ServiceResponse<WineResult>>
{
    private readonly IWineRepository _wineRepository;

    public UpdateWineCommandHandler(IWineRepository wineRepository)
    {
        _wineRepository = wineRepository;
    }

    public async Task<ServiceResponse<WineResult>> Handle(UpdateWineCommand request,
        CancellationToken cancellationToken)
    {
        return await _wineRepository.UpdateWineAsync(request.Id, request.UpdatedWine);
    }
}