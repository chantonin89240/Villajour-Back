using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Events.GetEventType;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Appointments.GetAppointmentType
{
    public record class GetAppointmentTypeCommand : IRequest<List<AppointmentTypeEntity>>
    {
    }

    public class GetAppointmentTypeHandler : IRequestHandler<GetAppointmentTypeCommand, List<AppointmentTypeEntity>>
    {
        private readonly IVillajourDbContext _context;

        public GetAppointmentTypeHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentTypeEntity?>> Handle(GetAppointmentTypeCommand request, CancellationToken cancellationToken)
        {
            List<AppointmentTypeEntity> entity = await _context.AppointmentTypes.ToListAsync(cancellationToken);

            return entity;
        }
    }
}
