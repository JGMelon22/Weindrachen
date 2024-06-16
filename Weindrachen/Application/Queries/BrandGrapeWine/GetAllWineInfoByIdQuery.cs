using MediatR;
using Weindrachen.DTOs.BrandGrapeWine;
using Weindrachen.Models;

namespace Weindrachen.Application.Queries.BrandGrapeWine;

public record GetAllWineInfoByIdQuery(int id) : IRequest<ServiceResponse<BrandGrapeWineResult>>;