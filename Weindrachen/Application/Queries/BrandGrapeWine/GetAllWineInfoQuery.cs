using MediatR;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Models;

namespace Weindrachen.Application.Queries.BrandGrapeWine;

public record GetAllWineInfoQuery : IRequest<ServiceResponse<IEnumerable<BrandGrapeWineResult>>>;