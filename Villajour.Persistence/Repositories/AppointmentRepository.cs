using Microsoft.EntityFrameworkCore;
using Villajour.Domain.Common;
using Villajour.Application.Interfaces;

namespace Villajour.Persistence.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly VilleajourDbContext _context;

        public AppointmentRepository(VilleajourDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentEntity>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<AppointmentEntity> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<AppointmentEntity> AddAppointmentAsync(AppointmentEntity appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<AppointmentEntity> UpdateAppointmentAsync(AppointmentEntity appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
