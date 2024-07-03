using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.Grape;
using Weindrachen.Application.Queries.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Queries.Grape;

public class GetGrapeByIdQueryHandlerTests
{
    private readonly IGrapeRepository _grapeRepository;

    public GetGrapeByIdQueryHandlerTests()
    {
        _grapeRepository = A.Fake<IGrapeRepository>();
    }

    [Fact]
    public async Task GetGrapeByIdQueryHandler_Handle_ReturnsGrape()
    {
        // Arrange 
        var grapeId = 1;
        var handler = new GetGrapeByIdHandler(_grapeRepository);
        var grapeResult = new GrapeResult
        {
            Id = grapeId,
            Name = "Bobal"
        };
        var serviceResponse = new ServiceResponse<GrapeResult>
        {
            Data = grapeResult
        };

        A.CallTo(() => _grapeRepository.GetGrapeByIdAsync(grapeId))
            .Returns(Task.FromResult(serviceResponse));

        var query = new GetGrapeByIdQuery(grapeId);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(grapeResult);
        A.CallTo(() => _grapeRepository.GetGrapeByIdAsync(grapeId))
            .MustHaveHappenedOnceExactly();
    }
}