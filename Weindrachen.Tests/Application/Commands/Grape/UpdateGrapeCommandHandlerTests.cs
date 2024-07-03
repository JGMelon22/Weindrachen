using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.Application.Handlers.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Commands.Grape;

public class UpdateGrapeCommandHandlerTests
{
    private readonly IGrapeRepository _grapeRepository;

    public UpdateGrapeCommandHandlerTests()
    {
        _grapeRepository = A.Fake<IGrapeRepository>();
    }

    [Fact]
    public async Task UpdateGrapeCommandHandler_Handle_ReturnsGrapeResult()
    {
        // Arrange
        int id = 1;
        var updatedGrape = new GrapeInput("Sangiovese");
        var handler = new UpdateGrapeHandler(_grapeRepository);
        var grapeResult = new GrapeResult
        {
            Id = id,
            Name = "Sangiovese"
        };
        var serviceResponse = new ServiceResponse<GrapeResult> { Data = grapeResult };

        A.CallTo(() => _grapeRepository.UpdateGrapeAsync(id, updatedGrape))
            .Returns(Task.FromResult(serviceResponse));

        var command = new UpdateGrapeCommand(id, updatedGrape);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().Be(grapeResult);
        A.CallTo(() => _grapeRepository.UpdateGrapeAsync(id, updatedGrape))
            .MustHaveHappenedOnceExactly();
    }
}