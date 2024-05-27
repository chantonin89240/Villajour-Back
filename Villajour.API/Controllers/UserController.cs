using Microsoft.AspNetCore.Mvc;
using Villajour.Application.Commands.AddUser;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            UserEntity user = await Mediator.Send(command);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("L'utilisateur ne peut pas être ajouté");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
}
