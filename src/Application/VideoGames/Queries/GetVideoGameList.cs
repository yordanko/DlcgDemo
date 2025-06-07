using System;
using Application.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.VideoGames;

public class GetVideoGameList
{
    public class Query : IRequest<List<VideoGameDto>>
    {
    }

   
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, List<VideoGameDto>>
    {


        public async Task<List<VideoGameDto>> Handle(Query request, CancellationToken cancellationToken)
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

}
