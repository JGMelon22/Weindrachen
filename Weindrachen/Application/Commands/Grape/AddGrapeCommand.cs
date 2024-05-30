using MediatR;
using Weindrachen.DTOs.Grape;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Grape;

public record AddGrapeCommand(GrapeInput NewGrape) : IRequest<ServiceResponse<GrapeResult>>;