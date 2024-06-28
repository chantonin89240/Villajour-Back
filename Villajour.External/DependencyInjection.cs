using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Villajour.Application.Chatbot.Interfaces;
using Villajour.Domain.Extension;
using Villajour.External.Chatbot;
using Villajour.External.Options;

namespace Villajour.External;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        AddOptions<OpenAi>(OpenAi.PropertyName);

        return services;

        void AddOptions<TOptions>(string propertyName)
            where TOptions : class
        {
            services.AddOptions<TOptions>(configuration.GetSection(propertyName));
        }
    }

    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        var OpenAIConfig = services.BuildServiceProvider().GetRequiredService<IOptions<OpenAi>>().Value;

        if (OpenAIConfig != null)
        {
            services.AddKernel()
                .AddAzureOpenAIChatCompletion(OpenAIConfig.CompletionDeploymentName, OpenAIConfig.Endpoint, OpenAIConfig.Key);
        }

        services.AddScoped<ISemanticKernelService, SemanticKernelService>();

        return services;
    }
}
