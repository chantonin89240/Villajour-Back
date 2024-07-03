using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Mairies.AddMairie;
using Villajour.Application.Commands.Mairies.DeleteMairie;
using Villajour.Application.Commands.Mairies.GetDetailMairie;
using Villajour.Application.Commands.Mairies.GetHomeMairie;
using Villajour.Application.Commands.Mairies.GetMairieById;
using Villajour.Application.Commands.Mairies.GetMairies;
using Villajour.Application.Commands.Mairies.UpdateMairie;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class MairieController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public MairieController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// fonction pour récupérer la mairie grâce a sont id
    /// </summary>
    /// <param name="id">Identifiant Guid de la mairie</param>
    /// <returns>Code http Ok et l'entité Mairie</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMairieById(Guid id)
    {
        if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetMairieByIdCommand command = new GetMairieByIdCommand();
            command.Id = id;
            var mairie = await _mediator.Send(command);

            if (mairie != null)
            {
                return Ok(mairie);
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
    /// fonction pour récupérer toutes les mairies
    /// </summary>
    /// <returns>Code http Ok et la liste des entités Mairie</returns>
    [HttpGet]
    public async Task<IActionResult> GetMairies()
    {
        try
        {
            GetMairiesCommand command = new GetMairiesCommand();
            List<MairieEntity> mairie = await _mediator.Send(command);

            return Ok(mairie);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour ajouter une mairie
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok et l'entité Mairie</returns>
    [HttpPost]
    public async Task<IActionResult> AddMairie([FromBody] AddMairieCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            MairieEntity mairie = await _mediator.Send(command);

            if (mairie != null)
            {
                return Ok(mairie);
            }
            else
            {
                return NotFound("La mairie ne peut pas être ajouté");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour la modification d'une mairie
    /// </summary>
    /// <param name="id">Identifiant Guid de la mairie</param>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok et l'entité Mairie</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMairie(Guid id, [FromBody] UpdateMairieCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        if (id != command.Id) return BadRequest("incorrect Guid.");

        try
        {
            var mairie = await _mediator.Send(command);

            if (mairie != null)
            {
                return Ok(mairie);
            }
            else
            {
                return NotFound("La mairie n'a pas pu être modifié !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour la suppression d'une mairie
    /// </summary>
    /// <param name="id">Identifiant Guid de la mairie</param>
    /// <returns>Code http Ok</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMairie(Guid id)
    {
        if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            DeleteMairieCommand command = new DeleteMairieCommand();
            command.Id = id;
            bool mairie = await _mediator.Send(command);

            if (mairie)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, "La mairie n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour récupérer le détail de la mairie
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="MairieId"></param>
    /// <returns>Code http Ok et objet DetailMairieDto</returns>
    [HttpGet("GetDetailMairie/{UserId}/{MairieId}")]
    public async Task<IActionResult> GetMairieFavByUser(Guid UserId, Guid MairieId)
    {
        if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect user Guid.");
        if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect mairie Guid.");

        try
        {
            GetDetailMairieCommand command = new GetDetailMairieCommand();
            command.UserId = UserId;
            command.MairieId = MairieId;
            DetailMairieDto dto = await _mediator.Send(command);

            if (dto != null)
            {
                return Ok(dto);
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
    /// fonction pour récupérer les info de la page home de la mairie
    /// </summary>
    /// <param name="MairieId"></param>
    /// <returns>Code http Ok et </returns>
    [HttpGet("GetHomeMairie/{MairieId}")]
    public async Task<IActionResult> GetHomeMairie(Guid MairieId)
    {
        if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect mairie Guid.");

        try
        {
            GetHomeMairieCommand command = new GetHomeMairieCommand();
            command.MairieId = MairieId;
            HomeMairieDto dto = await _mediator.Send(command);

            if (dto != null)
            {
                return Ok(dto);
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
