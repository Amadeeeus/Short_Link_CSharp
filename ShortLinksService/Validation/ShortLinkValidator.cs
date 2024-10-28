using FluentValidation;
using ShortLinksService.Controllers;
using ShortLinksService.Entities;

namespace ShortLinksService.Validation;

public class ShortLinkValidator:AbstractValidator<CreateLinkEntity>
{
    public ShortLinkValidator()
    {
        RuleFor(entity=>entity.Url).NotEmpty().WithMessage("Url cannot be empty");
    }
}