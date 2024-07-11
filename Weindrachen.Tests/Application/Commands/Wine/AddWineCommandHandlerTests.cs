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

public class AddWineCommandHandlerTests
{
    private readonly IWineRepository _wineRepository;

    public AddWineCommandHandlerTests()
    {
        _wineRepository = A.Fake<IWineRepository>();
    }

    [Fact]
    public async Task AddWineCommandHandler_Handle_ReturnsWineResult()
    {
        // Arrange
        var newWine = new WineInput("Passo Los Valles",
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
        var handler = new AddWineCommandHandler(_wineRepository);
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
        var serviceResponse = new ServiceResponse<WineResult>
        {
            Data = wineResult
        };

        A.CallTo(() => _wineRepository.AddNewWineAsync(newWine))
            .Returns(Task.FromResult(serviceResponse));

        var command = new AddWineCommand(newWine);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().BeEquivalentTo(wineResult);
        A.CallTo(() => _wineRepository.AddNewWineAsync(newWine)).MustHaveHappenedOnceExactly();
    }
}