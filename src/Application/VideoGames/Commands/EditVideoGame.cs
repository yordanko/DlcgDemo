using System;
using Application.Core;
using Application.Dtos;
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

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, VideoGameDto>
    {
        public async Task<VideoGameDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var videoGame = await context.VideoGames.FindAsync(request.VideoGameDto.Id);

            if (videoGame == null)
            {
                throw new AppException(404, "Video game not found");
            }

            // Update properties of videoGame here based on request
            mapper.Map(request.VideoGameDto, videoGame);

            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<VideoGameDto>(videoGame);
        }
    }

}
