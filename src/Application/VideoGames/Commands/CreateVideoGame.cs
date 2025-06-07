using System;
using Application.Dtos;
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
    
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var videoGame = mapper.Map<Domain.VideoGame>(request.VideoGameDto);
            context.VideoGames.Add(videoGame);
            await context.SaveChangesAsync(cancellationToken);

            return videoGame.Id;
        }

    }

}
