using FluentValidation;

namespace Villajour.Application.Commands.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(b => b.Picture).NotEmpty();
    }
}
