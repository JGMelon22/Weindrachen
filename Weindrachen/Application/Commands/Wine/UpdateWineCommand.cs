using MediatR;
using Weindrachen.DTOs.Wine;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Wine;

public record UpdateWineCommand(int Id, WineInput UpdatedWine) : IRequest<ServiceResponse<WineResult>>;