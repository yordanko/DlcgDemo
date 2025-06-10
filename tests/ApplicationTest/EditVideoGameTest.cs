using System;
using Application.Core;
using Application.Dtos;
using Application.Repository;
using Application.VideoGames.Commands;
using AutoFixture;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ApplicationTest;

public class EditVideoGameTest
{
    private readonly Mock<IGameRepository> _gameRepo;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;
    private readonly CancellationToken _cancellationToken;

    private readonly IMediator _mediator;

    public EditVideoGameTest()
    {
        _fixture = new Fixture();
        var mockMapper = new MapperConfiguration(mc =>
        {
            mc.AddMaps(typeof(MappingProfile).Assembly);
        }).CreateMapper().ConfigurationProvider;

        _mapper = new Mapper(mockMapper);

        _gameRepo = new Mock<IGameRepository>();

        _cancellationToken = new CancellationToken();

        var services = new ServiceCollection();
        var serviceProvider = services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(EditVideoGame).Assembly))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        
    }

    [Fact]
    public async Task ShouldReturn_WhenGameIdExist()
    {
        // Arrange
        var dto = _fixture.Create<EditVideoGameDto>();
        var dtoReturn = _fixture.Create<VideoGameDto>();
        _gameRepo.Setup(repo => repo.EditVideoGame(dto, _cancellationToken))
            .ReturnsAsync(dtoReturn);

        var query = new EditVideoGame.Command { VideoGameDto = dto }; // Required for identifying the video game 

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.IsType<VideoGameDto>(result);
        Assert.Equal(dtoReturn.Description, result.Description);

    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenGameIdDoesNotExist()
    {
        // Arrange
        var dto = _fixture.Create<EditVideoGameDto>();
        _gameRepo.Setup(repo => repo.EditVideoGame(dto, _cancellationToken))
            .Throws(new AppException(404, "Video game not found"));

        var query = new EditVideoGame.Command { VideoGameDto = dto }; // Required for identifying the video game 


        // Act & Assert
        await  Assert.ThrowsAsync<AppException>(()=>_mediator.Send(query));

    }

}
