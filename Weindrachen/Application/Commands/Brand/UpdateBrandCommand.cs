using MediatR;
using Weindrachen.DTOs.Brand;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Brand;

public record UpdateBrandCommand(int Id, BrandInput UpdatedBrand) :IRequest<ServiceResponse<BrandResult>>;