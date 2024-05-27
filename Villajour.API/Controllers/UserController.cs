using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.AddUser;
using Villajour.Application.Commands.DeleteUser;
using Villajour.Application.Commands.GetUserById;
using Villajour.Application.Commands.UpdateUser;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetUserByIdCommand command = new GetUserByIdCommand();
            command.Id = id;
            var user = await _mediator.Send(command);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("L'utilisateur n'existe pas !");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }


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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        if (id != command.Id) return BadRequest("incorrect Guid.");

        try
        {
            var user = await _mediator.Send(command);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("L'utilisateur n'a pas pu être modifié !");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            DeleteUserCommand command = new DeleteUserCommand();
            command.Id = id;
            bool user = await _mediator.Send(command);

            if (user)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, "L'utilisateur n'existe pas !");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
}
