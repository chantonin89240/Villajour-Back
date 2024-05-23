using Microsoft.AspNetCore.Mvc;
using Villajour.Application.Commands.AddScheduleMairie;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleMairieController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddScheduleMairieCommand command)
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
