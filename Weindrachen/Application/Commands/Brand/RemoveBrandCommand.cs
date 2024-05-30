using MediatR;
using Weindrachen.Models;

namespace Weindrachen.Application.Commands.Brand;

public record RemoveBrandCommand(int Id) : IRequest<ServiceResponse<bool>>;