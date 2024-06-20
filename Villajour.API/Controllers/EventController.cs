using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Events.AddEvent;
using Villajour.Application.Commands.Events.DeleteEvent;
using Villajour.Application.Commands.Events.GetEventByMairieDetail;
using Villajour.Application.Commands.Events.GetEventByMairieFavorite;
using Villajour.Application.Commands.Events.GetEventFavoriteByUser;
using Villajour.Application.Commands.Events.GetEventHistoByMairie;
using Villajour.Application.Commands.Events.GetEventType;
using Villajour.Application.Commands.Events.UpdateEvent;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class EventController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// fonction pour récupérer les events favorit d'un utilisateur
    /// </summary>
    /// <param name="UserId">Identifiant Guid du user</param>
    /// <returns>Liste des events que l'utilisateur a en favoris</returns>
    [HttpGet("GetEventFavoriteByUser/{UserId}")]
    public async Task<IActionResult> GetEventFavoriteByUser(Guid UserId)
    {
        if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetEventFavoriteByUserCommand command = new GetEventFavoriteByUserCommand();
            command.UserId = UserId;
            List<EventDto> eventEnt = await _mediator.Send(command);

           
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
    /// fonction pour récupérer tous les events de toutes les mairies qu'un utilisateur a en favorit
    /// </summary>
    /// <param name="UserId">Identifiant Guid du user</param>
    /// <returns>Dto EventByMairieFavorite</returns>
    [HttpGet("GetEventByMairieFavorite/{UserId}")]
    public async Task<IActionResult> GetEventByMairieFavorite(Guid UserId)
    {
        if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetEventByMairieFavoriteCommand command = new GetEventByMairieFavoriteCommand();
            command.UserId = UserId;
            List<EventByMairieFavoriteDto> eventEnt = await _mediator.Send(command);

            if (eventEnt != null)
            {
                return Ok(eventEnt);
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
    /// fonction pour récupérer tous les events d'un mairie
    /// </summary>
    /// <param name="MairieId">Identifiant Guid de la mairie</param>
    /// <returns>Liste de l'ensemble des events d'une mairies par ordre décroissant</returns>
    [HttpGet("GetEventHistoByMairie/{MairieId}")]
    public async Task<IActionResult> GetEventByMairie(Guid MairieId)
    {
        if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetEventHistoByMairieCommand command = new GetEventHistoByMairieCommand();
            command.MairieId = MairieId;
            List<EventDto> eventEnt = await _mediator.Send(command);

            if (eventEnt != null)
            {
                return Ok(eventEnt);
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
    /// fonction pour l'ajout d'un event
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>code http Ok avec l'entité Event</returns>
    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] AddEventCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            EventEntity eventEnt = await _mediator.Send(command);

            if (eventEnt != null)
            {
                return Ok(eventEnt);
            }
            else
            {
                return NotFound("L'événnement ne peut pas être ajouté");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour la modification d'un event
    /// </summary>
    /// <param name="id">Id de l'event</param>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok avec l'entité Event</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        if (id != command.Id) return BadRequest("incorrect Guid.");

        try
        {
            var eventEnt = await _mediator.Send(command);

            if (eventEnt != null)
            {
                return Ok(eventEnt);
            }
            else
            {
                return NotFound("L'événnement n'a pas pu être modifié !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour supprimer un event
    /// </summary>
    /// <param name="id">Identifiant id de l'event</param>
    /// <returns>Code http</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            DeleteEventCommand command = new DeleteEventCommand();
            command.Id = id;
            bool eventEnt = await _mediator.Send(command);

            if (eventEnt)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, "L'événnement n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour récupérer les events d'une mairie
    /// </summary>
    /// <param name="id">Identifiant Guid de la mairie</param>
    /// <returns>Code http Ok et liste d'events</returns>
    [HttpGet("GetEventByMairieDetail/{UserId}/{MairieId}")]
    public async Task<IActionResult> GetEventByMairieDetail(Guid UserId, Guid MairieId)
    {
        if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect user Guid.");
        if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect mairie Guid.");

        try
        {
            GetEventByMairieDetailCommand command = new GetEventByMairieDetailCommand();
            command.UserId = UserId;
            command.MairieId = MairieId;
            List<EventByMairieDetailDto> eventEnt = await Mediator.Send(command);

            if (eventEnt != null)
            {
                return Ok(eventEnt);
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

    /// <summary>
    /// fonction pour la liste des types d'event
    /// </summary>
    /// <param></param>
    /// <returns>Code http Ok et liste de type d'event</returns>
    [HttpGet("GetEventType")]
    public async Task<IActionResult> GetEventType()
    {
        try
        {
            GetEventTypeCommand command = new GetEventTypeCommand();
            List<EventTypeEntity> DocumentType = await Mediator.Send(command);

            return Ok(DocumentType);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
