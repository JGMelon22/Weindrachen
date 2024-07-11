using FakeItEasy;
using FluentAssertions;
using Weindrachen.Application.Commands.Wine;
using Weindrachen.Application.Handlers.Wine;
using Weindrachen.DTOs.GrapeWine;
using Weindrachen.DTOs.Wine;
using Weindrachen.Interfaces;
using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Tests.Application.Commands.Wine;

public class UpdateWineCommandHandlerTests
{
    private readonly IWineRepository _wineRepository;

    public UpdateWineCommandHandlerTests()
    {
        _wineRepository = A.Fake<IWineRepository>();
    }

    [Fact]
    public async Task UpdateWineCommandHandler_Handle_ReturnsWineResult()
    {
        // Arrange
        int id = 1;
        var updatedWine = new WineInput("Passo Los Valles",
            27.0M,
            true,
            13.0F,
            Country.Chile,
            1,
            new List<GrapeWineInput>
            {
                new(1)
            },
            Taste.Plum
        );
        var handler = new UpdateWineCommandHandler(_wineRepository);
        var wineResult = new WineResult
        {
            Id = 1,
            Name = "Passo Los Valles",
            Price = 27.0M,
            IsDoc = true,
            AlcoholicLevel = 13.0F,
            Country = Country.Chile,
            Taste = Taste.Plum
        };
        var serviceResponse = new ServiceResponse<WineResult> { Data = wineResult };

        A.CallTo(() => _wineRepository.UpdateWineAsync(id, updatedWine))
            .Returns(Task.FromResult(serviceResponse));

        var command = new UpdateWineCommand(id, updatedWine);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().Be(wineResult);
        A.CallTo(() => _wineRepository.UpdateWineAsync(id, updatedWine))
            .MustHaveHappenedOnceExactly();
    }
}