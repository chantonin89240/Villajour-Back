using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Villajour.Application.Commands.AddUser;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers
{
    [ApiController]
    public class UserController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            UserEntity result = await Mediator.Send(command);

            return result != null ? Ok(result) : NotFound();
        }
    }
}
