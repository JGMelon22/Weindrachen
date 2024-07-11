using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Handlers.Grape;
using Weindrachen.Application.Queries.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Queries.Grape;

public class GetGrapesQueryHandlerTests
{
    private readonly IGrapeRepository _grapeRepository;

    public GetGrapesQueryHandlerTests()
    {
        _grapeRepository = A.Fake<IGrapeRepository>();
    }

    [Fact]
    public async Task GetGrapesQueryHandler_Handle_ReturnsGrapesResult()
    {
        // Arrange
        var handler = new GetGrapesQueryHandler(_grapeRepository);
        var grapesResult = new List<GrapeResult>
        {
            new() { Id = 1, Name = "Bobal" },
            new() { Id = 2, Name = "Cabernet Sauvignon" },
            new() { Id = 3, Name = "Syrah" }
        };
        var serviceResponse = new ServiceResponse<IEnumerable<GrapeResult>>
        {
            Data = grapesResult
        };

        A.CallTo(() => _grapeRepository.GetAllGrapesAsync())
            .Returns(Task.FromResult(serviceResponse));

        var query = new GetGrapesQuery();

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(grapesResult);
        A.CallTo(() => _grapeRepository.GetAllGrapesAsync())
            .MustHaveHappened();
    }
}