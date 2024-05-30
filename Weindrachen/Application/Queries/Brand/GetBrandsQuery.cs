using MediatR;
using Weindrachen.DTOs.Brand;
using Weindrachen.Models;

namespace Weindrachen.Application.Queries.Brand;

public record GetBrandsQuery() : IRequest<ServiceResponse<IEnumerable<BrandResult>>>;