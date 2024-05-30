using MediatR;
using Weindrachen.DTOs.Wine;
using Weindrachen.Models;

namespace Weindrachen.Application.Queries.Wine;

public record GetWineByIdQuery(int Id) : IRequest<ServiceResponse<WineResult>>;