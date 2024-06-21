using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Appointments.GetAppointmentByMairie
{
    public record class GetAppointmentByMairieCommand : IRequest<List<AppointmentEntity>>
    {
        public Guid MairieId { get; set; }

    }

    public class GetAppointmentByMairieCommandHandler : IRequestHandler<GetAppointmentByMairieCommand, List<AppointmentEntity>>
    {
        private readonly IVillajourDbContext _context;

        public GetAppointmentByMairieCommandHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentEntity?>> Handle(GetAppointmentByMairieCommand request, CancellationToken cancellationToken)
        {
            List<AppointmentEntity> entity = await _context.Appointments.Where(m => m.MairieId == request.MairieId).ToListAsync(cancellationToken);

            return entity;
        }
    }
}
