using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Documents.AddDocument;
using Villajour.Application.Commands.Documents.DeleteDocument;
using Villajour.Application.Commands.Documents.GetDocumentByFavoriteMairie;
using Villajour.Application.Commands.Documents.GetDocumentFav;
using Villajour.Application.Commands.Documents.GetDocumentHistoByMairie;
using Villajour.Application.Commands.Events.GetEventByMairieFavorite;
using Villajour.Application.Commands.Mairies.DeleteMairie;
using Villajour.Application.Commands.Mairies.GetMairieById;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class DocumentController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// fonction pour ajouter un document
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok et l'entité Document</returns>
    [HttpPost]
    public async Task<IActionResult> AddDocument([FromBody] AddDocumentCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            DocumentEntity document = await _mediator.Send(command);

            if (document != null)
            {
                return Ok(document);
            }
            else
            {
                return NotFound("Le document ne peut pas être ajouté");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour la suppression d'un document
    /// </summary>
    /// <param name="id">Identifiant int du document</param>
    /// <returns>Code http Ok</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            DeleteDocumentCommand command = new DeleteDocumentCommand();
            command.Id = id;
            bool Document = await _mediator.Send(command);

            if (Document)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, "Le document n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour récupérer tous les documents de toutes les mairies qu'un utilisateur a en favorit
    /// </summary>
    /// <param name="UserId">Identifiant Guid du user</param>
    /// <returns></returns>
    [HttpGet("GetDocumentByMairieFavorite/{UserId}")]
    public async Task<IActionResult> GetDocumentByMairieFavorite(Guid UserId)
    {
        if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetDocumentByMairieFavoriteCommand command = new GetDocumentByMairieFavoriteCommand();
            command.UserId = UserId;
            var eventEnt = await _mediator.Send(command);

            if (eventEnt != null)
            {
                return Ok(eventEnt);
            }
            else
            {
                return NotFound("Le user n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour récupérer les documents favorit d'un utilisateur
    /// </summary>
    /// <param name="id">Identifiant Guid du user</param>
    /// <returns>Code http Ok et liste d'entité Documents</returns>
    [HttpGet("GetDocumentFav/{UserId}")]
    public async Task<IActionResult> GetDocumentFav(Guid UserId)
    {
        if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetDocumentFavCommand command = new GetDocumentFavCommand();
            command.UserId = UserId;
            List<DocumentEntity> document = await _mediator.Send(command);

            if (document != null)
            {
                return Ok(document);
            }
            else
            {
                return NotFound("Le user n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour récupérer les documents d'une mairie
    /// </summary>
    /// <param name="id">Identifiant Guid de la mairie</param>
    /// <returns>Code http Ok et liste de document</returns>
    [HttpGet("GetDocumentHistoByMairie/{MairieId}")]
    public async Task<IActionResult> GetDocumentHistoByMairie(Guid MairieId)
    {
        if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetDocumentHistoByMairieCommand command = new GetDocumentHistoByMairieCommand();
            command.MairieId = MairieId;
            List<DocumentEntity> Document = await _mediator.Send(command);

            if (Document != null)
            {
                return Ok(Document);
            }
            else
            {
                return NotFound("La mairie n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
