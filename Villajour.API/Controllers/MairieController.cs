using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.AddMairie;
using Villajour.Application.Commands.DeleteMairie;
using Villajour.Application.Commands.GetMairieById;
using Villajour.Application.Commands.GetMairies;
using Villajour.Application.Commands.UpdateMairie;
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
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetMairies()
    {
        try
        {
            GetMairiesCommand command = new GetMairiesCommand();
            List<MairieEntity> mairie = await _mediator.Send(command);

            return Ok(mairie);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

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
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }


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
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

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
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
}
