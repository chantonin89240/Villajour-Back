using FluentValidation;

namespace Villajour.Application.Commands.Appointments.GetAppointmentByMairie
{
    public class GetAppointmentByMairieCommandValidator : AbstractValidator<GetAppointmentByMairieCommand>
    {
        public GetAppointmentByMairieCommandValidator()
        {
            RuleFor(c => c.MairieId).NotEmpty();
        }
    }
}
