using FluentValidation;

namespace Villajour.Application.Commands.Users.AddFavoriteContent;

public class AddFavoriteContentCommandValidator : AbstractValidator<AddFavoriteContentCommand>
{
    public AddFavoriteContentCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}
