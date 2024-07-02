using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Documents.GetDocumentType;

namespace Villajour.Application.Commands.Appointments.GetAppointmentType
{
    public class GetAppointmentTypeCommandValidator : AbstractValidator<GetAppointmentTypeCommand>
    {
        public GetAppointmentTypeCommandValidator()
        {
                
        }
    }
}
