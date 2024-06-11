using System.ComponentModel.DataAnnotations;

namespace Villajour.External.Options;

public record Chat
{
    public const string PropertyName = "Chat";

    [Required]
    public required string MaxConversationTokens { get; init; }

    [Required]
    public required string CacheSimilarityScore { get; init; }
}
