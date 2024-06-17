using FluentValidation;

namespace Villajour.Application.Commands.Users.DeleteFavoriteContent;

public class DeleteFavoriteContentCommandValidator : AbstractValidator<DeleteFavoriteContentCommand>
{
    public DeleteFavoriteContentCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}