using FluentValidation;

namespace Villajour.Application.Commands.Appointments.GetAppointmentByUser
{
    public class GetAppointmentByUserCommandValidator : AbstractValidator<GetAppointmentByUserCommand>
    {
        public GetAppointmentByUserCommandValidator() 
        {
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}
