using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Villajour.Application.Commands.Appointments.AddAppointment
{
    public record class AddAppointmentCommand : IRequest<AppointmentEntity>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Title { get; set; }
        public string Statut { get; set; }
        public string? Description { get; set; }
        public int AppointmentTypeId { get; set; }
        public Guid MairieId { get; set; }
        public Guid UserId { get; set; }
    }

    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, AppointmentEntity>
    {
        private readonly IVillajourDbContext _context;

        public AddAppointmentCommandHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentEntity> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
            // Vérification des chevauchements et des horaires identiques
            var overlappingAppointments = _context.Appointments
                .Where(a => a.MairieId == request.MairieId &&
                            (a.StartTime == request.StartTime || a.EndTime == request.EndTime ||
                             (a.StartTime < request.EndTime && a.EndTime > request.StartTime)))
                .ToList();

            if (overlappingAppointments.Any())
            {
                var overlappingEntity = new AppointmentEntity
                {
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Title = request.Title,
                    Statut = "chevauchement",
                    Description = request.Description,
                    AppointmentTypeId = request.AppointmentTypeId,
                    MairieId = request.MairieId,
                    UserId = request.UserId
                };

                return overlappingEntity;
            }

            var entity = new AppointmentEntity
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Title = request.Title,
                Statut = request.Statut,
                Description = request.Description,
                AppointmentTypeId = request.AppointmentTypeId,
                MairieId = request.MairieId,
                UserId = request.UserId
            };

            _context.Appointments.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
