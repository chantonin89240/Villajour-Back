using FluentValidation;

namespace Villajour.Application.Commands.AddMairie;

public class UpdateMairieCommandValidator : AbstractValidator<AddMairieCommand>
{
    public UpdateMairieCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty().Matches("^[0-9]+$").MinimumLength(10).MaximumLength(10); 
        RuleFor(c => c.Siret).NotEmpty().Matches("^[0-9]+$").MinimumLength(14).MaximumLength(14);
        RuleFor(c => c.Address).NotEmpty();
    }
}
