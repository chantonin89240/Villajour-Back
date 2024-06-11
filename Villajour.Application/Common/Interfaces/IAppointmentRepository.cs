using Villajour.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Villajour.Application.Common.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<AppointmentEntity>> GetAllAppointmentsAsync();
        Task<AppointmentEntity> GetAppointmentByIdAsync(int id);
        Task<AppointmentEntity> AddAppointmentAsync(AppointmentEntity appointment);
        Task<AppointmentEntity> UpdateAppointmentAsync(AppointmentEntity appointment);
        Task DeleteAppointmentAsync(int id);
    }
}
