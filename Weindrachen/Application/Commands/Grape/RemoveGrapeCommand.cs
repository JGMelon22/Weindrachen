using MediatR;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Grape;

public record RemoveGrapeCommand(int Id) : IRequest<ServiceResponse<bool>>;