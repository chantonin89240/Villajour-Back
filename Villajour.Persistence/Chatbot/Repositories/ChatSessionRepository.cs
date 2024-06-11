using Villajour.Application.Chatbot.Interfaces;
using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Persistence.Chatbot.Repositories;

/// <summary>
/// A repository for chat sessions.
/// </summary>
public class ChatSessionRepository : Repository<ChatSession>, IChatSessionRepository
{
    /// <summary>
    /// Initializes a new instance of the ChatSessionRepository class.
    /// </summary>
    /// <param name="storageContext">The storage context.</param>
    public ChatSessionRepository(IStorageContext<ChatSession> storageContext)
        : base(storageContext)
    {
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ChatSession>> GetAllChatsAsync()
    {
        return StorageContext.QueryEntitiesAsync(e => true);
    }
}
