using System;
using Application.Dtos;
using Domain;

namespace Application.Repository;

public interface IGameRepository
{
    Task<List<VideoGameDto>> GetVideoGames(CancellationToken cancellationToken);
    Task<VideoGameDto> GetVideoGame(string id,CancellationToken cancellationToken);
    Task<VideoGameDto> EditVideoGame(EditVideoGameDto videoGame,CancellationToken cancellationToken);
    Task<bool> DeleteVideoGame(string id, CancellationToken cancellationToken);

    Task<string> AddVideoGame(CreateVideoGameDto videoGame, CancellationToken cancellationToken);
}
