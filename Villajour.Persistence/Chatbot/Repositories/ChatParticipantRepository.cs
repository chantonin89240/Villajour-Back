using Villajour.Application.Chatbot.Interfaces;
using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Persistence.Chatbot.Repositories;

/// <summary>
/// A repository for chat sessions.
/// </summary>
public class ChatParticipantRepository : Repository<ChatParticipant>, IChatParticipantRepository
{
    /// <summary>
    /// Initializes a new instance of the ChatParticipantRepository class.
    /// </summary>
    /// <param name="storageContext">The storage context.</param>
    public ChatParticipantRepository(IStorageContext<ChatParticipant> storageContext)
        : base(storageContext)
    {
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ChatParticipant>> FindByUserIdAsync(string userId)
    {
        return StorageContext.QueryEntitiesAsync(e => e.UserId == userId);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ChatParticipant>> FindByChatIdAsync(string chatId)
    {
        return StorageContext.QueryEntitiesAsync(e => e.ChatId == chatId);
    }

    /// <inheritdoc/>
    public async Task<bool> IsUserInChatAsync(string userId, string chatId)
    {
        var users = await StorageContext.QueryEntitiesAsync(e => e.UserId == userId && e.ChatId == chatId);
        return users.Any();
    }
}
