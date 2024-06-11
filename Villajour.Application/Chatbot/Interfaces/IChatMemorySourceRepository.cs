using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Application.Chatbot.Interfaces;

/// <summary>
/// A repository for chat messages.
/// </summary>
public interface IChatMemorySourceRepository : IRepository<MemorySource>
{
    /// <summary>
    /// Finds chat memory sources by chat session id
    /// </summary>
    /// <param name="chatId">The chat session id.</param>
    /// <param name="includeGlobal">Flag specifying if global documents should be included in the response.</param>
    /// <returns>A list of memory sources.</returns>
    Task<IEnumerable<MemorySource>> FindByChatIdAsync(string chatId, bool includeGlobal = true);

    /// <summary>
    /// Finds chat memory sources by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>A list of memory sources with the given name.</returns>
    Task<IEnumerable<MemorySource>> FindByNameAsync(string name);

    /// <summary>
    /// Retrieves all memory sources.
    /// </summary>
    /// <returns>A list of memory sources.</returns>
    Task<IEnumerable<MemorySource>> GetAllAsync();
}