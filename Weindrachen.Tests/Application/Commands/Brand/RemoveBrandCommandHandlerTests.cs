using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Brand;
using Weindrachen.Application.Handlers.Brand;
using Weindrachen.Interfaces;
using Weindrachen.Models;

namespace Weindrachen.Tests.Application.Commands.Brand;

public class RemoveBrandCommandHandlerTests
{
    private readonly IBrandRepository _brandRepository;

    public RemoveBrandCommandHandlerTests()
    {
        _brandRepository = A.Fake<IBrandRepository>();
    }

    [Fact]
    public async Task RemoveBrandCommandHandler_Handle_ReturnsSuccess()
    {
        // Arrange
        var id = 1;
        var handler = new RemoveBrandHandler(_brandRepository);

        var serviceResponse = new ServiceResponse<bool>();

        A.CallTo(() => _brandRepository.RemoveBrandAsync(id))
            .Returns(Task.FromResult(serviceResponse));

        var command = new RemoveBrandCommand(id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        A.CallTo(() => _brandRepository.RemoveBrandAsync(id))
            .MustHaveHappenedOnceExactly();
    }
}