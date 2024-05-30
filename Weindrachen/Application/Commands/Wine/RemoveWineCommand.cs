using MediatR;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Wine;

public record RemoveWineCommand(int Id) : IRequest<ServiceResponse<bool>>;