﻿namespace Villajour.Application.Commands.Dto;

public class DeleteFavoriteContentDto
{
    public Guid UserId { get; set; }
    public int? AnnouncementId { get; set; }
    public int? EventId { get; set; }
    public int? DocumentId { get; set; }
}
