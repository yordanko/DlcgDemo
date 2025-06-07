using System;
using Application.Dtos;
using Application.VideoGames.Commands;

namespace Application.Validators;

public class CreateVideoGameValidator :
    BaseVideoGameValidator<CreateVideoGame.Command, CreateVideoGameDto>
{
    public CreateVideoGameValidator(): base(x=>x.VideoGameDto)
    {
        
    }

}
