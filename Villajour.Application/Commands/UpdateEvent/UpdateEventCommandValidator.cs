﻿using FluentValidation;

namespace Villajour.Application.Commands.UpdateEvent;

public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.EventTypeId).NotEmpty().Must(value => value is int).WithMessage("La valeur de la propriété doit être de type entier.");
        RuleFor(c => c.MairieId).NotEmpty().Must(value => value is Guid).WithMessage("La valeur de la propriété doit être de type Guid.");
    }
}
