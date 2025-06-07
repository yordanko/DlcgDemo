using System;
using Application.Core;
using Application.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.VideoGames.Queries;

public class GetVideoGameDetails
{
    public class Query : IRequest<VideoGameDto>
    {
        public string Id { get; set; } = string.Empty; // Required for identifying the video game
    }
    public class Handler(AppDbContext context, IMapper mapper   ) : IRequestHandler<Query, VideoGameDto>
    {
        public async Task<VideoGameDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var videoGame = await context.VideoGames.
                ProjectTo<VideoGameDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(vg => vg.Id == request.Id, cancellationToken);


            if (videoGame == null)
            {
                 throw new AppException(404, "Video game not found");
            }

            return videoGame;
        }
    }
}
