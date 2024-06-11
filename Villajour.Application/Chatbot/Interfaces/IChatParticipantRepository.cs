using Villajour.Domain.Entities.Chatbot;

namespace Villajour.Application.Chatbot.Interfaces;

/// <summary>
/// A repository for chat sessions.
/// </summary>
public interface IChatParticipantRepository : IRepository<ChatParticipant>
{
    /// <summary>
    /// Finds chat participants by user id.
    /// A user can be part of multiple chats, thus a user can have multiple chat participants.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A list of chat participants of the same user id in different chat sessions.</returns>
    Task<IEnumerable<ChatParticipant>> FindByChatIdAsync(string chatId);

    /// <summary>
    /// Finds chat participants by chat id.
    /// </summary>
    /// <param name="chatId">The chat id.</param>
    /// <returns>A list of chat participants in the same chat sessions.</returns>
    Task<IEnumerable<ChatParticipant>> FindByUserIdAsync(string userId);

    /// <summary>
    /// Checks if a user is in a chat session.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="chatId">The chat id.</param>
    /// <returns>True if the user is in the chat session, false otherwise.</returns>
    Task<bool> IsUserInChatAsync(string userId, string chatId);
}