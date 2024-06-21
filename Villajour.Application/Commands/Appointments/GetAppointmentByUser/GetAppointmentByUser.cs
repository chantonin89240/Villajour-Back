using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Appointments.GetAppointmentByUser
{
    public record class GetAppointmentByUserCommand : IRequest<List<AppointmentEntity>>
    {
        public Guid UserId { get; set; }
    }


    public class GetAppointmentByUserCommandHandler : IRequestHandler<GetAppointmentByUserCommand, List<AppointmentEntity>>
        {
            private readonly IVillajourDbContext _context;

            public GetAppointmentByUserCommandHandler(IVillajourDbContext context)
            {
                _context = context;
            }

            public async Task<List<AppointmentEntity?>> Handle(GetAppointmentByUserCommand request, CancellationToken cancellationToken)
            {
                List<AppointmentEntity> entity = await _context.Appointments.Where(m => m.UserId == request.UserId).OrderByDescending(e => e.EndTime).ToListAsync(cancellationToken);

                return entity;
            }
        }
}
