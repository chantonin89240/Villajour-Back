using FluentValidation;
namespace Villajour.Application.Commands.Appointments.ValidateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(c => c.StartTime).NotEmpty();
            RuleFor(c => c.AppointmentTypeId).NotEmpty().Must(value => value is int).WithMessage("La valeur de la propriété doit être de type entier.");
            RuleFor(c => c.MairieId).NotEmpty().Must(value => value is Guid).WithMessage("La valeur de la propriété doit être de type Guid.");
            RuleFor(c => c.Statut).NotEmpty();
        }
    }
}
