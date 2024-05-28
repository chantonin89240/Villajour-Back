using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.AddMairie;
using Villajour.Application.Commands.DeleteEvent;
using Villajour.Application.Commands.DeleteMairie;
using Villajour.Application.Commands.GetMairieById;
using Villajour.Application.Commands.GetMairies;
using Villajour.Application.Commands.UpdateEvent;
using Villajour.Application.Commands.UpdateMairie;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetMairieByIdCommand command = new GetMairieByIdCommand();
            command.Id = id;
            var eventEnt = await _mediator.Send(command);

            if (eventEnt != null)
            {
                return Ok(eventEnt);
            }
            else
            {
                return NotFound("La mairie n'existe pas !");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        try
        {
            GetMairiesCommand command = new GetMairiesCommand();
            List<MairieEntity> eventEnt = await _mediator.Send(command);

            return Ok(eventEnt);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

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
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }


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
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

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
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
}
