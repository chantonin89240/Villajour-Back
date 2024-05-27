using FluentValidation;

namespace Villajour.Application.Commands.UpdateUser;

public class deleteUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public deleteUserCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Phone).Matches("^[0-9]+$").MaximumLength(10);
    }
}
