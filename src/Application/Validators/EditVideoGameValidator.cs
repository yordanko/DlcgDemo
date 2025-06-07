using System;
using Application.Dtos;
using Application.VideoGames.Commands;

namespace Application.Validators;

public class EditVideoGameValidator : 
    BaseVideoGameValidator<EditVideoGame.Command, EditVideoGameDto>
{
    public EditVideoGameValidator() : base(x => x.VideoGameDto)
    {
        
    }
}
