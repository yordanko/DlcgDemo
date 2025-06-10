using System;
using Application.Core;
using Application.Dtos;
using Application.Repository;
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
    public class Handler(IGameRepository repository  ) : IRequestHandler<Query, VideoGameDto>
    {
        public async Task<VideoGameDto> Handle(Query request, CancellationToken cancellationToken)
        {
            return await repository.GetVideoGame(request.Id, cancellationToken);
        }
    }
}
