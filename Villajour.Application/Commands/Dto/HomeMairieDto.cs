using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto;

public class HomeMairieDto
{
    public AnnouncementDto? Announcement { get; set; }
    public EventDto? Event { get; set; }
    public DocumentDto? Document { get; set; }
}
