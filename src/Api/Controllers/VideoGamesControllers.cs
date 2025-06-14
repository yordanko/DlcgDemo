using Application.Dtos;
using Application.VideoGames;
using Application.VideoGames.Commands;
using Application.VideoGames.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class VideoGamesController : BaseApiController
{
    //Get: all videogames
    [HttpGet]
    public async Task<ActionResult<List<VideoGameDto>>> GetAllVideoGames()
    {
        // Logic to retrieve all video games

        return HandleResult(await Mediator.Send(new GetVideoGameList.Query()));
    }

    // GET: api/videogame
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGameDto>> GetVideoGameById(string id)
    {
        // Logic to retrieve a video game by its ID

        return HandleResult(await Mediator.Send(new GetVideoGameDetails.Query { Id = id }));
    }

    // POST: api/videogame
    [HttpPost]
    public async Task<ActionResult<string>> CreateVideoGame([FromBody] CreateVideoGameDto videoGameDto)
    {
        return HandleResult(
            await Mediator.Send(new CreateVideoGame.Command { VideoGameDto = videoGameDto }));
    }

    // PUT: api/videogame/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateVideoGame(string id, [FromBody] EditVideoGameDto videoGame)
    {
        videoGame.Id = id; // Ensure the ID is set for the video game being edited
        return HandleResult(
            await Mediator.Send(new EditVideoGame.Command { VideoGameDto = videoGame }));

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteVideoGame(string id)
    {
        return HandleResult(
           await Mediator.Send(new DeleteVideoGame.Command { Id = id }));
        
    }

}
