using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Users.AddFavoriteContent;
using Villajour.Application.Commands.Users.AddFavoriteMairie;
using Villajour.Application.Commands.Users.AddUser;
using Villajour.Application.Commands.Users.DeleteFavoriteContent;
using Villajour.Application.Commands.Users.DeleteFavoriteMairie;
using Villajour.Application.Commands.Users.DeleteUser;
using Villajour.Application.Commands.Users.GetMairieFavByUser;
using Villajour.Application.Commands.Users.GetUserById;
using Villajour.Application.Commands.Users.UpdateUser;
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

    /// <summary>
    /// fonction pour récupéré un utilisateur avec sont id
    /// </summary>
    /// <param name="id">Identifiant Guid de l'utilisateur</param>
    /// <returns>l'entité User</returns>
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
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour ajouter un utilisateur
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok avec l'entité User</returns>
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
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour mettre à jour un utilisateur
    /// </summary>
    /// <param name="id">Identifiant Guid de l'utilisateur</param>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok avec l'entité User</returns>
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
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour supprimer un utilisateur
    /// </summary>
    /// <param name="id">Identifiant Guid de l'utilisateur</param>
    /// <returns>Code http Ok</returns>
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
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour ajouter un favoris
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok</returns>
    [HttpPost("AddFavoriteContent")]
    public async Task<IActionResult> AddFavoriteContent([FromBody] AddFavoriteContentCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            FavoriteContentEntity favoris = await _mediator.Send(command);

            if (favoris != null)
            {
                return Ok();
            }
            else
            {
                return NotFound("L'utilisateur ne peut pas être ajouté");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour ajouter une mairie favoris
    /// </summary>
    /// <param name="command">Propriété de la command</param>
    /// <returns>Code http Ok</returns>
    [HttpPost("AddFavoriteMairie")]
    public async Task<IActionResult> AddFavoriteMairie([FromBody] AddFavoriteMairieCommand command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            FavoriteMairieEntity favoris = await _mediator.Send(command);

            if (favoris != null)
            {
                return Ok();
            }
            else
            {
                return NotFound("L'utilisateur ne peut pas être ajouté");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// fonction pour supprimer un favoris
    /// </summary>
    /// <param name="command">Dto de la command</param>
    /// <returns>Code http Ok</returns>
    [HttpDelete("DeleteFavoriteContent")]
    public async Task<IActionResult> DeleteFavoriteContent([FromBody] DeleteFavoriteContentDto command)
    {
        if(command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            DeleteFavoriteContentCommand commandExec = new DeleteFavoriteContentCommand();
            commandExec.UserId = command.UserId;
            commandExec.DocumentId = command.DocumentId;
            commandExec.AnnouncementId = command.AnnouncementId;
            commandExec.EventId = command.EventId;
            bool fav = await _mediator.Send(commandExec);

            if (fav)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, "Le favoris n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour supprimer une mairie favoris
    /// </summary>
    /// <param name="command">Dto de la command</param>
    /// <returns>Code http Ok</returns>
    [HttpDelete("DeleteFavoriteMairie")]
    public async Task<IActionResult> DeleteFavoriteMairie([FromBody] DeleteFavoriteMairieDto command)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null.");
        }

        try
        {
            DeleteFavoriteMairieCommand commandExec = new DeleteFavoriteMairieCommand();
            commandExec.UserId = command.UserId;
            commandExec.MairieId = command.MairieId;
            bool fav = await _mediator.Send(commandExec);

            if (fav)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, "Le favoris n'existe pas !");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// fonction pour récupérer tous les mairies favoris du user
    /// </summary>
    /// <param name="UserId">Identifiant Guid du user</param>
    /// <returns>Dto UserMairieFav</returns>
    [HttpGet("GetMairieFavByUser/{UserId}")]
    public async Task<IActionResult> GetMairieFavByUser(Guid UserId)
    {
        if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

        try
        {
            GetMairieFavByUserCommand command = new GetMairieFavByUserCommand();
            command.UserId = UserId;
            List<MairieEntity> user = await _mediator.Send(command);

            if (user != null)
            {
                return Ok(user);
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
}
