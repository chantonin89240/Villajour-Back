using Villajour.Domain.Common;
using System.Collections.Generic;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentEntity>> GetAllAppointmentsAsync();
    Task<AppointmentEntity> GetAppointmentByIdAsync(int id);
    Task<AppointmentEntity> AddAppointmentAsync(AppointmentEntity appointment);
    Task<AppointmentEntity> UpdateAppointmentAsync(AppointmentEntity appointment);
    Task DeleteAppointmentAsync(int id);
}
