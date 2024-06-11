using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Villajour.Persistence.Chatbot;
using Villajour.Domain.Entities.Chatbot;
using Villajour.Domain.Extension;
using Villajour.Application.Chatbot.Interfaces;
using Villajour.Persistence.Chatbot.Repositories;
using Villajour.Persistence.Chatbot.Options;
using Villajour.Application.Interfaces;
using Villajour.Persistence.Repositories;

namespace Villajour.Persistence;

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
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        var cosmosConfig = services.BuildServiceProvider().GetRequiredService<IOptions<CosmosDb>>().Value;

        var chatSessionStorageContext = new CosmosDbContext<ChatSession>(
            cosmosConfig.ConnectionString, cosmosConfig.Database, cosmosConfig.ChatSessionsContainer);

        var chatMessageStorageContext = new CosmosDbContext<ChatMessage>(
            cosmosConfig.ConnectionString, cosmosConfig.Database, cosmosConfig.ChatMessagesContainer);

        var chatMemorySourceStorageContext = new CosmosDbContext<MemorySource>(
            cosmosConfig.ConnectionString, cosmosConfig.Database, cosmosConfig.ChatMemorySourcesContainer);

        var chatParticipantStorageContext = new CosmosDbContext<ChatParticipant>(
            cosmosConfig.ConnectionString, cosmosConfig.Database, cosmosConfig.ChatParticipantsContainer);

        services.AddScoped<IChatSessionRepository, ChatSessionRepository>(provider => new ChatSessionRepository(chatSessionStorageContext));
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>(provider => new ChatMessageRepository(chatMessageStorageContext));
        services.AddScoped<IChatMemorySourceRepository, ChatMemorySourceRepository>(provider => new ChatMemorySourceRepository(chatMemorySourceStorageContext));
        services.AddScoped<IChatParticipantRepository, ChatParticipantRepository>(provider => new ChatParticipantRepository(chatParticipantStorageContext));
        
        return services;
    }

    public static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        AddOptions<CosmosDb>(CosmosDb.PropertyName);

        return services;

        void AddOptions<TOptions>(string propertyName)
            where TOptions : class
        {
            services.AddOptions<TOptions>(configuration.GetSection(propertyName));
        }
    }
}
