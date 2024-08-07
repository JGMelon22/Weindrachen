using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.Application.Handlers.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Commands.Wine;

public class RemoveWineCommandHandlerTests
{
    private readonly IWineRepository _wineRepository;

    public RemoveWineCommandHandlerTests()
    {
        _wineRepository = A.Fake<IWineRepository>();
    }

    [Fact]
    public async Task RemoveWineCommandHandler_Handle_ReturnsSuccess()
    {
        // Arrange
        int id = 1;
        var handler = new RemoveWineCommandHandler(_wineRepository);

        var serviceResponse = new ServiceResponse<bool>();

        A.CallTo(() => _wineRepository.RemoveWineAsync(id))
            .Returns(Task.FromResult(serviceResponse));

        var command = new RemoveWineCommand(id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        A.CallTo(() => _wineRepository.RemoveWineAsync(id))
            .MustHaveHappenedOnceExactly();
    }
}