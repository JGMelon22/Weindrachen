using MediatR;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Models;

namespace Weindrachen.Application.Queries.BrandGrapeWine;

public record GetAllWineInfoByIdQuery(int Id) : IRequest<ServiceResponse<BrandGrapeWineResult>>;