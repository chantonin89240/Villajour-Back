using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Documents.AddDocument;
using Villajour.Application.Commands.Documents.DeleteDocument;
using Villajour.Application.Commands.Documents.GetDocumentByFavoriteMairie;
using Villajour.Application.Commands.Documents.GetDocumentFav;
using Villajour.Application.Commands.Documents.GetDocumentHistoByMairie;
using Villajour.Application.Commands.Documents.GetDocumentType;
using Villajour.Application.Commands.Dto;
using Villajour.Domain.Common;
using Azure.Storage.Blobs;
using Azure.Storage;
using Villajour.Application.Commands.Documents.GetDocumentByMairieDetail;

namespace Villajour.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class DocumentController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private IConfiguration configuration;

    public DocumentController(IMediator mediator, IConfiguration configuration)
    {
        this._mediator = mediator;
        this.configuration = configuration;
    }

    /// <summary>
    /// fonction pour ajouter un document
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok et l'entité Document</returns>
    [HttpPost]
    public async Task<IActionResult> AddDocument([FromForm] AddDocumentDto command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        var connectionString = configuration["storage:connectionString"];
        var containerName = configuration["storage:containerName"];
        var blobServiceClient = new BlobServiceClient(connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await blobContainerClient.CreateIfNotExistsAsync();

        var blobClient = blobContainerClient.GetBlobClient(command.Document.FileName);

        using (var stream = command.Document.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        AddDocumentCommand DocumentCommand = new AddDocumentCommand();
        DocumentCommand.Title = command.Title;
        DocumentCommand.Description = command.Description;
        DocumentCommand.DocumentUrl = blobClient.Uri.ToString();
        DocumentCommand.DocumentTypeId = command.DocumentTypeId;
        DocumentCommand.MairieId = command.MairieId;

        try
        {
            DocumentEntity document = await _mediator.Send(DocumentCommand);

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
                return NotFound("L'utilisateur n'existe pas !");
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
            List<DocumentDto> document = await _mediator.Send(command);

            if (document != null)
            {
                return Ok(document);
            }
            else
            {
                return NotFound("L'utilisateur n'existe pas !");
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
            List<DocumentDto> Document = await _mediator.Send(command);

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

    /// <summary>
    /// fonction pour la liste des types de document
    /// </summary>
    /// <param></param>
    /// <returns>Code http Ok et liste de type de document</returns>
    [HttpGet("GetDocumentType")]
    public async Task<IActionResult> GetDocumentType()
    {
        try
        {
            GetDocumentTypeCommand command = new GetDocumentTypeCommand();
            List<DocumentTypeEntity> DocumentType = await Mediator.Send(command);

            return Ok(DocumentType);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Fonction pour télécharger le document
    /// </summary>
    /// <param name="fileUrl">Url du conteneur azure</param>
    /// <returns>le document</returns>
    [HttpGet("DownloadDocument")]
    public async Task<IActionResult> DownloadDocument(string fileUrl)
    {
        if (string.IsNullOrEmpty(fileUrl))
        {
            return BadRequest("Url du document vide.");
        }

        var blobUri = new Uri(fileUrl);
        var blobClient = new BlobClient(blobUri, new StorageSharedKeyCredential(configuration["storage:accountName"], configuration["storage:key"]));

        try
        {
            var downloadInfo = await blobClient.DownloadAsync();
            return File(downloadInfo.Value.Content, downloadInfo.Value.ContentType, blobClient.Name);
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
    [HttpGet("GetDocumentByMairieDetail/{UserId}/{MairieId}")]
    public async Task<IActionResult> GetDocumentByMairieDetail(Guid UserId, Guid MairieId)
    {
        if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");
        if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetDocumentByMairieDetailCommand command = new GetDocumentByMairieDetailCommand();
            command.UserId = UserId;
            command.MairieId = MairieId;
            List<DocumentByMairieDetailDto> document = await Mediator.Send(command);

            if (document != null)
            {
                return Ok(document);
            }
            else
            {
                return NotFound("il n'y a pas de document");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
