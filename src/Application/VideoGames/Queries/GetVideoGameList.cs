using System;
using Application.Dtos;
using Application.Repository;
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

   
    public class Handler( IGameRepository repository) : IRequestHandler<Query, List<VideoGameDto>>
    {
        public async Task<List<VideoGameDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await repository.GetVideoGames(cancellationToken);
        }
    }

}
