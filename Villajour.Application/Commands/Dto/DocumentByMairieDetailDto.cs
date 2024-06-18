﻿using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto;

public class DocumentByMairieDetailDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DocumentUrl { get; set; }
    public DocumentTypeEntity? DocumentType { get; set; }
    public bool Favorite { get; set; }
}