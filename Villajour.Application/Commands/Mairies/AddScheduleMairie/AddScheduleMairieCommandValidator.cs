using FluentValidation;

namespace Villajour.Application.Commands.Mairies.AddScheduleMairie;

public class AddScheduleMairieCommandValidator : AbstractValidator<AddScheduleMairieCommand>
{
    public AddScheduleMairieCommandValidator()
    {
        RuleFor(b => b.Date).NotEmpty();
        RuleFor(b => b.StartTime).NotEmpty();
        RuleFor(b => b.EndTime).NotEmpty();
        RuleFor(b => b.MairieId).NotEmpty();
    }
}
