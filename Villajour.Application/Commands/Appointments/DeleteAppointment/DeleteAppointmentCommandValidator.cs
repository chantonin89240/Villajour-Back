using FluentValidation;

namespace Villajour.Application.Commands.Appointments.DeleteAppointment
{
    public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
    {
        public DeleteAppointmentCommandValidator() 
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
