using Villajour.Application.Chatbot.Interfaces;
using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Persistence.Chatbot.Repositories;

/// <summary>
/// A repository for chat messages.
/// </summary>
public class ChatMemorySourceRepository : Repository<MemorySource>, IChatMemorySourceRepository
{
    /// <summary>
    /// Initializes a new instance of the ChatMemorySourceRepository class.
    /// </summary>
    /// <param name="storageContext">The storage context.</param>
    public ChatMemorySourceRepository(IStorageContext<MemorySource> storageContext)
        : base(storageContext)
    {
    }

    /// <inheritdoc/>
    public Task<IEnumerable<MemorySource>> FindByChatIdAsync(string chatId, bool includeGlobal = true)
    {
        return StorageContext.QueryEntitiesAsync(e => e.ChatId == chatId || includeGlobal && e.ChatId == Guid.Empty.ToString());
    }

    /// <inheritdoc/>
    public Task<IEnumerable<MemorySource>> FindByNameAsync(string name)
    {
        return StorageContext.QueryEntitiesAsync(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc/>
    public Task<IEnumerable<MemorySource>> GetAllAsync()
    {
        return StorageContext.QueryEntitiesAsync(e => true);
    }
}
