using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.Application.Handlers.Grape;
using Weindrachen.DTOs.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Commands.Grape;

public class AddGrapeCommandHandlerTests
{
    private readonly IGrapeRepository _grapeRepository;

    public AddGrapeCommandHandlerTests()
    {
        _grapeRepository = A.Fake<IGrapeRepository>();
    }

    [Fact]
    public async Task AddGrapeCommandHandler_Handle_ReturnsGrapeResult()
    {
        // Arrange
        var newGrape = new GrapeInput("Pinot noir");
        var handler = new AddGrapeHandler(_grapeRepository);
        var grapeResult = new GrapeResult
        {
            Id = 1,
            Name = "Pinot noir"
        };
        var serviceResponse = new ServiceResponse<GrapeResult>
        {
            Data = grapeResult
        };

        A.CallTo(() => _grapeRepository.AddNewGrapeAsync(newGrape))
            .Returns(Task.FromResult(serviceResponse));

        var command = new AddGrapeCommand(newGrape);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(grapeResult);
        A.CallTo(() => _grapeRepository.AddNewGrapeAsync(newGrape))
            .MustHaveHappenedOnceExactly();
    }
}