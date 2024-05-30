using MediatR;
using Weindrachen.DTOs.Grape;
using Weindrachen.Models;

namespace Weindrachen.Application.Queries.Grape;

public record GetGrapesQuery() :IRequest<ServiceResponse<IEnumerable<GrapeResult>>>;