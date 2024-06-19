using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto;

public class EventByMairieDetailDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Address { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public EventTypeEntity? EventType { get; set; }
    public bool Favorite { get; set; }
}
