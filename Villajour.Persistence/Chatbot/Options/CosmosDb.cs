using System.ComponentModel.DataAnnotations;

namespace Villajour.Persistence.Chatbot.Options;

/// <summary>
/// Configuration settings for connecting to Azure CosmosDB.
/// </summary>
public record CosmosDb
{
    public const string PropertyName = "CosmoDb";

    /// <summary>
    /// Gets or sets the Cosmos database name.
    /// </summary>
    [Required]
    public required string Database { get; init; }

    /// <summary>
    /// Gets or sets the Cosmos connection string.
    /// </summary>
    [Required]
    public required string ConnectionString { get; init; }

    /// <summary>
    /// Gets or sets the Cosmos container for chat sessions.
    /// </summary>
    [Required]
    public required string ChatSessionsContainer { get; init; }

    /// <summary>
    /// Gets or sets the Cosmos container for chat messages.
    /// </summary>
    [Required]
    public required string ChatMessagesContainer { get; init; }

    /// <summary>
    /// Gets or sets the Cosmos container for chat memory sources.
    /// </summary>
    [Required]
    public required string ChatMemorySourcesContainer { get; init; }

    /// <summary>
    /// Gets or sets the Cosmos container for chat participants.
    /// </summary>
    [Required]
    public required string ChatParticipantsContainer { get; init; }
}
