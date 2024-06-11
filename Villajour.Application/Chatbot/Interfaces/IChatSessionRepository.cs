using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Application.Chatbot.Interfaces;

/// <summary>
/// A repository for chat sessions.
/// </summary>
public interface IChatSessionRepository : IRepository<ChatSession>
{
    /// <summary>
    /// Retrieves all chat sessions.
    /// </summary>
    /// <returns>A list of ChatMessages.</returns>
    Task<IEnumerable<ChatSession>> GetAllChatsAsync();
}