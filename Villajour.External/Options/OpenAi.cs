using System.ComponentModel.DataAnnotations;

namespace Villajour.External.Options;

public record OpenAi
{
    public const string PropertyName = "OpenAi";

    [Required]
    public required string Endpoint { get; init; }

    [Required]
    public required string Key { get; init; }

    [Required]
    public required string CompletionDeploymentName { get; init; }
}
