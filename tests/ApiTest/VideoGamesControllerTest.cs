using System;
using Api.Controllers;
using Application.Dtos;
using Application.Repository;
using Application.VideoGames.Commands;
using AutoFixture;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ApiTest;

public class VideoGamesControllerTest
{
    private readonly IMediator _mediator;
    private readonly VideoGamesController _controller;
    private readonly Fixture _fixture;
    private readonly Mock<IGameRepository> _gameRepo;
    private readonly CancellationToken _cancellationToken;

    public VideoGamesControllerTest()
    {
        _controller = new VideoGamesController();
        _fixture = new Fixture();
        var services = new ServiceCollection();
        var serviceProvider = services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(EditVideoGame).Assembly))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _gameRepo = new Mock<IGameRepository>();
        _cancellationToken = new CancellationToken();

    }

    [Fact]
    public async Task GetGames_WithNoParams_Returns10Games()
    {
        // arrange
        var games = _fixture.CreateMany<VideoGameDto>(10).ToList();
        _gameRepo.Setup(repo => repo.GetVideoGames(_cancellationToken)).ReturnsAsync(games);

        // act
        var result = await _controller.GetAllVideoGames();

        // assert
        Assert.Equal(10, result.Value?.Count());
        Assert.IsType<ActionResult<VideoGame[]>>(result);
    }

    [Fact]
    public async Task GetGames_ById_ReturnsGame()
    {
        // arrange
        var game = _fixture.Create<VideoGameDto>();
        _gameRepo.Setup(repo => repo.GetVideoGame(It.IsAny<string>(), _cancellationToken)).ReturnsAsync(game);

        // act
        var result = await _controller.GetVideoGameById(game.Id);

        // assert
        Assert.Equal(game.Description, result.Value?.Description);
        Assert.IsType<ActionResult<VideoGame[]>>(result);
    }
    

}
