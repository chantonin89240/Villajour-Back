using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Villajour.Domain.Extension;

namespace Villajour.Domain.Extension;

public static class OptionExtension
{
    /// <summary>
    /// Parse configuration into options.
    /// </summary>
    public static void AddOptions<TOptions>(this IServiceCollection services, IConfigurationSection section)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
