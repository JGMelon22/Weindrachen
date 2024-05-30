using MediatR;
using Weindrachen.DTOs.Wine;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Wine;

public record AddWineCommand(WineInput NewWine) : IRequest<ServiceResponse<WineResult>>;