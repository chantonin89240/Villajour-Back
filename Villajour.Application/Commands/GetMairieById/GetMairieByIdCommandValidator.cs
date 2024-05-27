using FluentValidation;
using Villajour.Application.Commands.UpdateMairie;

namespace Villajour.Application.Commands.GetMairieById;

public class GetMairiesCommandValidator : AbstractValidator<GetMairieByIdCommand>
{
    public GetMairiesCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
