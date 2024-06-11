using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto;

public class DocumentDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public byte[]? Document { get; set; }
    public DocumentTypeEntity? DocumentType { get; set; }
    public MairieEntity? Mairie { get; set; }
}
