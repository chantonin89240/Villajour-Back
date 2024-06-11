using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Persistence.Repositories;
using Villajour.Persistence.Interfaces;
using Villajour.Application.Common.Interfaces;

namespace Villajour.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<VilleajourDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IVilleajourDbContext, VilleajourDbContext>();

            services.AddScoped<VilleajourDbContextInitialiser>();
            services.AddScoped<Application.Common.Interfaces.IAppointmentRepository, AppointmentRepository>();

            return services;
        }
    }
}
