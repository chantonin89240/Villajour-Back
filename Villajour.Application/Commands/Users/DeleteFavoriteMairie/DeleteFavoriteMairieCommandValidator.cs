using FluentValidation;

namespace Villajour.Application.Commands.Users.DeleteFavoriteMairie;

public class DeleteFavoriteMairieCommandValidator : AbstractValidator<DeleteFavoriteMairieCommand>
{
    public DeleteFavoriteMairieCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}


