using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

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

            return services;
        }
    }
}
