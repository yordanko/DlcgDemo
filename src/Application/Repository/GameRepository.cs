using System;
using Application.Core;
using Application.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class GameRepository(AppDbContext context, IMapper mapper) : IGameRepository
{
    async public Task<string> AddVideoGame(CreateVideoGameDto videoGameDto, CancellationToken cancellationToken)
    {
        var videoGame = mapper.Map<Domain.VideoGame>(videoGameDto);
        context.VideoGames.Add(videoGame);
        await context.SaveChangesAsync(cancellationToken);

        return videoGame.Id;
    }

    async public Task<bool> DeleteVideoGame(string id, CancellationToken cancellationToken)
    {
        var game = await context.VideoGames.FindAsync(id, cancellationToken)
                                ?? throw new AppException(404, "Video game not found");
        context.Remove(game);

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    async public Task<VideoGameDto> EditVideoGame(EditVideoGameDto videoGame, CancellationToken cancellationToken)
    {
        //get game from repository
        var game = await context.VideoGames.FindAsync(videoGame.Id);

        if (game == null)
        {
            throw new AppException(404, "Video game not found");
        }

        // Update properties of videoGame here based on request
        var gameDto = mapper.Map(videoGame, game);

        //save
        await context.SaveChangesAsync(cancellationToken);
        return mapper.Map<VideoGameDto>(game);
    }

    async public Task<VideoGameDto> GetVideoGame(string id, CancellationToken cancellationToken)
    {
        var videoGame = await context.VideoGames.
                ProjectTo<VideoGameDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(vg => vg.Id == id, cancellationToken);


        if (videoGame == null)
        {
                throw new AppException(404, "Video game not found");
        }

        return videoGame;
    }

    async public Task<List<VideoGameDto>> GetVideoGames(CancellationToken cancellationToken)
    {
        var query = context.VideoGames
                .OrderBy(vg => vg.Title)
                .AsQueryable();

        // Apply any filtering or pagination logic here if needed
        var games = query
            .ProjectTo<VideoGameDto>(mapper.ConfigurationProvider);
    
        return await games.ToListAsync(cancellationToken);

    }
}
