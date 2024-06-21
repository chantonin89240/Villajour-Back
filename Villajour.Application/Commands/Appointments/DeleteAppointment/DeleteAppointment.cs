using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Appointments.DeleteAppointment
{
    public record class DeleteAppointmentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
    {
        private readonly IVillajourDbContext _context;

        public DeleteAppointmentCommandHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public class DeleteEntity
        {
            public bool ConfirmationDelete { get; set; }
        }

        public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                _context.Appointments.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
