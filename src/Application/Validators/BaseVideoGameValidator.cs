using System;
using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class BaseVideoGameValidator<T, TDto> : AbstractValidator<T> where TDto : BaseVideoGameDto
{
    public BaseVideoGameValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title must be less than 100 characters.");

        RuleFor(x => selector(x).Genre)
            .NotEmpty()
            .WithMessage("Genre is required.")
            .MaximumLength(50)
            .WithMessage("Genre must be less than 50 characters.");

        RuleFor(x => selector(x).ImageUrl)
            .NotEmpty()
            .WithMessage("Image URL is required.")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Image URL must be a valid absolute URL.");

        RuleFor(x => selector(x).Platform)
            .NotEmpty()
            .WithMessage("Platform is required.")
            .MaximumLength(50)
            .WithMessage("Platform must be less than 50 characters.");

        RuleFor(x => selector(x).ReleaseDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Release date cannot be in the future.");
        
        RuleFor(x => selector(x).Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters.");
        
    }
}
