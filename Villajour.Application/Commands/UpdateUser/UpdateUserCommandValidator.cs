using FluentValidation;

namespace Villajour.Application.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Phone).Matches("^[0-9]+$").MaximumLength(10);
    }
}
