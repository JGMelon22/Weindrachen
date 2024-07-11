using MediatR;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Wine;

public class AddWineCommandHandler : IRequestHandler<AddWineCommand, ServiceResponse<WineResult>>
{
    private readonly IWineRepository _wineRepository;

    public AddWineCommandHandler(IWineRepository wineRepository)
    {
        _wineRepository = wineRepository;
    }

    public async Task<ServiceResponse<WineResult>> Handle(AddWineCommand request, CancellationToken cancellationToken)
    {
        return await _wineRepository.AddNewWineAsync(request.NewWine);
    }
}