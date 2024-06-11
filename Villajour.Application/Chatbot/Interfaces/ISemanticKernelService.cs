using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Application.Chatbot.Interfaces;

public interface ISemanticKernelService
{
    IAsyncEnumerable<string?> ChatAsync(Guid userId, Guid chatId, string message);
    Task<Guid> CreateChatSessionAsync(Guid userId);
    Task<List<ChatSession>> GetAllChatSessionsAsync(Guid userId);
    Task<List<ChatMessage>?> GetChatMessagesAsync(Guid chatId);
    Task<string> SummarizeConversationAsync(string conversation);
}
