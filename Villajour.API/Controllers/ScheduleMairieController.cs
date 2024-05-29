using Microsoft.AspNetCore.Mvc;
using Villajour.Application.Commands.Mairies.AddScheduleMairie;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleMairieController : ApiControllerBase
{
    /// <summary>
    /// fonction pour l'ajout des horaires de la mairie
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok et l'entité ScheduleMairie</returns>
    [HttpPost]
    public async Task<IActionResult> AddSchedule([FromBody] AddScheduleMairieCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            ScheduleMairieEntity scheduleMairie = await Mediator.Send(command);

            if (scheduleMairie != null)
            {
                return Ok(scheduleMairie);
            }
            else
            {
                return NotFound("L'horaire de la mairie ne peut pas être ajouté");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
