using System;
using Application.Dtos;
using Application.Repository;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.VideoGames.Commands;

public class CreateVideoGame
{
    public class Command : IRequest<string>
    {
        public required CreateVideoGameDto VideoGameDto { get; set; } // Required for creating a new video game
    }
    
    public class Handler(IGameRepository repository) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            return await repository.AddVideoGame(request.VideoGameDto, cancellationToken);
        }

    }

}
