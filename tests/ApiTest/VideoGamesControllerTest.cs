using System;
using Api.Controllers;
using Application.Dtos;
using Application.Repository;
using Application.VideoGames;
using Application.VideoGames.Commands;
using AutoFixture;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ApiTest;

public class VideoGamesControllerTest
{
    private  Mock<IMediator> _mediator;
    private readonly VideoGamesController _controller ;

    private readonly Fixture _fixture;
    private readonly Mock<IGameRepository> _gameRepo;
    private readonly CancellationToken _cancellationToken;
    public readonly ServiceProvider _services;

    public VideoGamesControllerTest()
    {
        _fixture = new Fixture();
        _gameRepo = new Mock<IGameRepository>();
        _cancellationToken = new CancellationToken();
        _mediator = new Mock<IMediator>();
        var services = new ServiceCollection();
        services.AddSingleton(_gameRepo.Object);
        _services = services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(EditVideoGame.Handler).Assembly))
            .BuildServiceProvider();
        _controller = new VideoGamesController()
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = _services
                }
            }
        };
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
        Assert.IsType<ActionResult<List<VideoGameDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var gamesList = Assert.IsAssignableFrom<List<VideoGameDto>>(okResult.Value);
        Assert.Equal(10, gamesList.Count);
        
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
        Assert.IsType<ActionResult<VideoGameDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var gameReturn  = Assert.IsAssignableFrom<VideoGameDto>(okResult.Value);
        Assert.Equal(game.Description, gameReturn.Description);
        
    }
    

}
