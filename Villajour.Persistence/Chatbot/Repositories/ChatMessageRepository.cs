using Villajour.Application.Chatbot.Interfaces;
using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Persistence.Chatbot.Repositories;

/// <summary>
/// A repository for chat messages.
/// </summary>
public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    /// <summary>
    /// Initializes a new instance of the ChatMessageRepository class.
    /// </summary>
    /// <param name="storageContext">The storage context.</param>
    public ChatMessageRepository(IStorageContext<ChatMessage> storageContext)
        : base(storageContext)
    {
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ChatMessage>> FindByChatIdAsync(string chatId)
    {
        return StorageContext.QueryEntitiesAsync(e => e.ChatId == chatId);
    }

    /// <inheritdoc/>
    public async Task<ChatMessage> FindLastByChatIdAsync(string chatId)
    {
        var chatMessages = await FindByChatIdAsync(chatId);
        var first = chatMessages.MaxBy(e => e.Timestamp);
        return first ?? throw new KeyNotFoundException($"No messages found for chat '{chatId}'.");
    }
}
