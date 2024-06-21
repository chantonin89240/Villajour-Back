using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Appointments.ValidateAppointment
{
    public record class UpdateAppointmentCommand : IRequest<AppointmentEntity>
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Title { get; set; }
        public string Statut { get; set; }
        public string? Description { get; set; }
        public int AppointmentTypeId { get; set; }
        public Guid MairieId { get; set; }
        public Guid UserId { get; set; }

    }

    public class ValidateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentEntity>
    {
        private readonly IVilleajourDbContext _context;

        public ValidateAppointmentCommandHandler(IVilleajourDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentEntity?> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                entity.StartTime = request.StartTime;
                entity.EndTime = request.EndTime;
                entity.Statut = request.Statut;
                entity.Title = request.Title;
                entity.Description = request.Description;
                entity.AppointmentTypeId = request.AppointmentTypeId;
                entity.MairieId = request.MairieId;

                await _context.SaveChangesAsync(cancellationToken);

                return entity;
            }
            else
            {
                return null;
            }
        }
    }
}
