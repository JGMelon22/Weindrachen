using MediatR;
using Weindrachen.DTOs.Grape;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Grape;

public record UpdateGrapeCommand(int Id, GrapeInput UpdatedGrape) :IRequest<ServiceResponse<GrapeResult>>;