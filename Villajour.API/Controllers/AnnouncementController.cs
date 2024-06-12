using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Announcements.AddAnnouncement;
using Villajour.Application.Commands.Announcements.DeleteAnnouncement;
using Villajour.Application.Commands.Announcements.GetAnnoucementByFavoriteMaire;
using Villajour.Application.Commands.Announcements.GetAnnouncementFav;
using Villajour.Application.Commands.Announcements.UpdateAnnouncement;
using Villajour.Domain.Common;

namespace Villajour.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AnnouncementController: ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// fonction pour ajouter une annonce
        /// </summary>
        /// <param name="command">Propriété de la command</param>
        /// <returns>Code http Ok et l'entité Announcement</returns>
        [HttpPost]
        public async Task<IActionResult> AddAnnouncementt([FromBody] AddAnnouncementCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            try
            {
                AnnouncementEntity announcement = await _mediator.Send(command);

                if (announcement != null)
                {
                    return Ok(announcement);
                }
                else
                {
                    return NotFound("L'annonce ne peut pas être ajoutée");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// fonction pour la suppression d'une annonce
        /// </summary>
        /// <param name="id">Identifiant int du announcement</param>
        /// <returns>Code http Ok</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                DeleteAnnouncementCommand command = new DeleteAnnouncementCommand();
                command.Id = id;
                bool announcement = await _mediator.Send(command);

                if (announcement)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(400, "L'annonce n'existe pas !");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// fonction pour récupérer toutes les annonces de toutes les mairies qu'un utilisateur a en favoris
        /// </summary>
        /// <param name="UserId">Identifiant Guid du user</param>
        /// <returns></returns>
        [HttpGet("GetAnnouncementByMairieFavorite/{UserId}")]
        public async Task<IActionResult> GetAnnouncementByMairieFavorite(Guid UserId)
        {
            if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                GetAnnoucementByFavoriteMaireCommand command = new GetAnnoucementByFavoriteMaireCommand();
                command.UserId = UserId;
                var announcementEnt = await _mediator.Send(command);

                if (announcementEnt != null)
                {
                    return Ok(announcementEnt);
                }
                else
                {
                    return NotFound("Le user n'existe pas !");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// fonction pour récupérer les annonces favoris d'un utilisateur
        /// </summary>
        /// <param name="id">Identifiant Guid du user</param>
        /// <returns>Code http Ok et liste d'entité Announcement</returns>
        [HttpGet("GetAnnouncementFav/{UserId}")]
        public async Task<IActionResult> GetAnnouncementFav(Guid UserId)
        {
            if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                GetAnnouncementFavCommand command = new GetAnnouncementFavCommand();
                command.UserId = UserId;
                List<AnnouncementEntity> announcement = await _mediator.Send(command);

                if (announcement != null)
                {
                    return Ok(announcement);
                }
                else
                {
                    return NotFound("Le user n'existe pas !");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// fonction pour la modification d'une annonce
        /// </summary>
        /// <param name="id">Id de l'annonce</param>
        /// <param name="command">Propriété de la command</param>
        /// <returns>Code http Ok avec l'entité announcement</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] UpdateAnnouncementCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            if (id != command.Id) return BadRequest("incorrect Guid.");

            try
            {
                var announcement = await _mediator.Send(command);

                if (announcement != null)
                {
                    return Ok(announcement);
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
    }
}
