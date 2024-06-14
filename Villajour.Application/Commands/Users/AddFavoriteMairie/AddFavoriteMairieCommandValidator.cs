using FluentValidation;

namespace Villajour.Application.Commands.Users.AddFavoriteMairie;

public class AddFavoriteMairieCommandValidator : AbstractValidator<AddFavoriteMairieCommand>
{
    public AddFavoriteMairieCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
