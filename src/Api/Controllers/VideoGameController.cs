using System;
using System.Threading.Tasks;
using Application.Dtos;
using Application.VideoGames;
using Application.VideoGames.Commands;
using Application.VideoGames.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class VideoGameController : BaseApiController
{
    //Get: all videogames
    [HttpGet]
    public async Task<ActionResult> GetAllVideoGames()
    {
        // Logic to retrieve all video games
        
        return HandleResult(await Mediator.Send(new GetVideoGameList.Query()));
    }

    // GET: api/videogame
    [HttpGet("{id}")]
    public async Task<ActionResult> GetVideoGameById(string id)
    {
        // Logic to retrieve a video game by its ID
        
         return HandleResult(await Mediator.Send(new GetVideoGameDetails.Query { Id = id }));
    }

    // POST: api/videogame
    [HttpPost]
    public async Task<ActionResult> CreateVideoGame([FromBody] CreateVideoGameDto videoGameDto)
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

}
