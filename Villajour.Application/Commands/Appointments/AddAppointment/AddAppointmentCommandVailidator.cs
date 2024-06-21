using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Events.AddEvent;

namespace Villajour.Application.Commands.Appointments.AddAppointment
{
    public class AddAppointmentCommandVailidator : AbstractValidator<AddAppointmentCommand>
    {
        public AddAppointmentCommandVailidator()
        {
            RuleFor(c => c.StartTime).NotEmpty();
            RuleFor(c => c.Statut).NotEmpty();
            RuleFor(c => c.AppointmentTypeId).NotEmpty().Must(value => value is int).WithMessage("La valeur de la propriété doit être de type entier.");
            RuleFor(c => c.MairieId).NotEmpty().Must(value => value is Guid).WithMessage("La valeur de la propriété doit être de type Guid.");
        }
      
    }
}
