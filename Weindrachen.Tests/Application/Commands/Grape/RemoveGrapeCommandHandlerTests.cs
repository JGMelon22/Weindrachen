using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Grape;
using Weindrachen.Application.Handlers.Grape;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Commands.Grape;

public class RemoveGrapeCommandHandlerTests
{
    private readonly IGrapeRepository _grapeRepository;

    public RemoveGrapeCommandHandlerTests()
    {
        _grapeRepository = A.Fake<IGrapeRepository>();
    }

    [Fact]
    public async Task RemoveGrapeCommandHandler_Handle_ReturnsSuccess()
    {
        // Arrange
        int id = 1;
        var handler = new RemoveGrapeHandler(_grapeRepository);

        var serviceResponse = new ServiceResponse<bool>();

        A.CallTo(() => _grapeRepository.RemoveGrapeAsync(id))
            .Returns(Task.FromResult(serviceResponse));

        var command = new RemoveGrapeCommand(id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        A.CallTo(() => _grapeRepository.RemoveGrapeAsync(id))
            .MustHaveHappenedOnceExactly();
    }
}