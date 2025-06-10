using System;
using System.Runtime.InteropServices;
using Application.Core;
using Application.Dtos;
using Application.Repository;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.VideoGames.Commands;

public class DeleteVideoGame
{
    public class Command : IRequest<bool>
    {
        public required string Id{ get; set; }
    }

    public class Handler(IGameRepository repository) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            return await repository.DeleteVideoGame(request.Id, cancellationToken);
        }
    }
}