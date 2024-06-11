using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Application.Chatbot.Interfaces;

/// <summary>
/// A repository for chat messages.
/// </summary>
public interface IChatMessageRepository : IRepository<ChatMessage>
{
    /// <summary>
    /// Finds chat messages by chat id.
    /// </summary>
    /// <param name="chatId">The chat id.</param>
    /// <returns>A list of ChatMessages matching the given chatId.</returns>
    Task<IEnumerable<ChatMessage>> FindByChatIdAsync(string chatId);

    /// <summary>
    /// Finds the most recent chat message by chat id.
    /// </summary>
    /// <param name="chatId">The chat id.</param>
    /// <returns>The most recent ChatMessage matching the given chatId.</returns>
    Task<ChatMessage> FindLastByChatIdAsync(string chatId);
}