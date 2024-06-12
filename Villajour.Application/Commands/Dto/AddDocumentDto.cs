using Microsoft.AspNetCore.Http;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto;

public class AddDocumentDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? Document { get; set; }
    public int DocumentTypeId { get; set; }
    public Guid MairieId { get; set; }
}
