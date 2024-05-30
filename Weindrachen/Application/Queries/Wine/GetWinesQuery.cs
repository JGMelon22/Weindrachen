using MediatR;
using Weindrachen.DTOs.Wine;
using Weindrachen.Models;

namespace Weindrachen.Application.Queries.Wine;

public record GetWinesQuery : IRequest<ServiceResponse<IEnumerable<WineResult>>>;