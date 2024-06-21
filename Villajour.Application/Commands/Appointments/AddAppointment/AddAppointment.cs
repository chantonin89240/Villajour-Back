using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

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
        private readonly IVilleajourDbContext _context;

        public AddAppointmentCommandHandler(IVilleajourDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentEntity> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
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
