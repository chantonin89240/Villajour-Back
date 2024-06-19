using Villajour.Application.Interfaces;
using Villajour.Domain.Common;

namespace Villajour.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentEntity>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        public async Task<AppointmentEntity> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        public async Task<AppointmentEntity> AddAppointmentAsync(AppointmentEntity appointment)
        {
            return await _appointmentRepository.AddAppointmentAsync(appointment);
        }

        public async Task<AppointmentEntity> UpdateAppointmentAsync(AppointmentEntity appointment)
        {
            return await _appointmentRepository.UpdateAppointmentAsync(appointment);
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
        }
    }
}
