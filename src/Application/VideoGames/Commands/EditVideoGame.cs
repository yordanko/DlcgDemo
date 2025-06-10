using System;
using Application.Core;
using Application.Dtos;
using Application.Repository;
using AutoMapper;
using MediatR;
using Persistence;


namespace Application.VideoGames.Commands;

public class EditVideoGame
{

    public class Command : IRequest<VideoGameDto>
    {
        public required EditVideoGameDto VideoGameDto { get; set; } // Required for identifying the video game 
    }

    public class Handler(IGameRepository repository) : IRequestHandler<Command, VideoGameDto>
    {
        public async Task<VideoGameDto> Handle(Command request, CancellationToken cancellationToken)
        {
            return await repository.EditVideoGame(request.VideoGameDto, cancellationToken);    
        }
    }

}
