﻿using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto;

public class AnnouncementByMairieDetailDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public AnnouncementTypeEntity? AnnouncementType { get; set; }
    public bool Favorite { get; set; }
}
