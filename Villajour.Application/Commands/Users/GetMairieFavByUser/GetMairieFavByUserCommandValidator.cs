using FluentValidation;
using Villajour.Application.Commands.Events.GetEventByMairieDetail;

namespace Villajour.Application.Commands.Users.GetMairieFavByUser;

public class GetMairieFavByUserCommandValidator : AbstractValidator<GetMairieFavByUserCommand>
{
    public GetMairieFavByUserCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}