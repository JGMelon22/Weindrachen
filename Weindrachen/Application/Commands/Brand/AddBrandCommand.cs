using MediatR;
using Weindrachen.DTOs.Brand;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Brand;

public record AddBrandCommand(BrandInput NewBrand) : IRequest<ServiceResponse<BrandResult>>;