using FluentValidation;

namespace Villajour.Application.Commands.GetMairieById;

public class GetMairieByIdCommandValidator : AbstractValidator<GetMairieByIdCommand>
{
    public GetMairieByIdCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
