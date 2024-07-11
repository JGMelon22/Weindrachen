using MediatR;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Application.Handlers.Wine;

public class RemoveWineCommandHandler : IRequestHandler<RemoveWineCommand, ServiceResponse<bool>>
{
    private readonly IWineRepository _wineRepository;

    public RemoveWineCommandHandler(IWineRepository wineRepository)
    {
        _wineRepository = wineRepository;
    }

    public async Task<ServiceResponse<bool>> Handle(RemoveWineCommand request, CancellationToken cancellationToken)
    {
        return await _wineRepository.RemoveWineAsync(request.Id);
    }
}