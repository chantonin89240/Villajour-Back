using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Villajour.External;
using Villajour.Persistence;

namespace Villajour.API.Extensions;

public static class ProgramExtensions
{
    public static void RegisterConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddExternalConfiguration(builder.Configuration);
        builder.Services.AddPersistenceConfiguration(builder.Configuration);
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddExternalServices();
        builder.Services.AddPersistenceServices(builder.Configuration);
    }

    public static AuthenticationBuilder Test(this AuthenticationBuilder builder, Action<IServiceProvider, JwtBearerOptions> action) 
    {
        return builder;
    }
}
