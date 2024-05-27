using FluentValidation;

namespace Villajour.Application.Commands.AddMairie;

public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    public AddEventCommandValidator()
    {
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.EventTypeId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
