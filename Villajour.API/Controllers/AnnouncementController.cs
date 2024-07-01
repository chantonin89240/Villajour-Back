using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Announcements.AddAnnouncement;
using Villajour.Application.Commands.Announcements.DeleteAnnouncement;
using Villajour.Application.Commands.Announcements.GetAnnouncementByMairieDetail;
using Villajour.Application.Commands.Announcements.GetAnnouncementByMairieFavorite;
using Villajour.Application.Commands.Announcements.GetAnnouncementFavoriteByUser;
using Villajour.Application.Commands.Announcements.GetAnnouncementHistoByMairie;
using Villajour.Application.Commands.Announcements.GetAnnouncementType;
using Villajour.Application.Commands.Announcements.UpdateAnnouncement;
using Villajour.Application.Commands.Dto;
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
        public async Task<IActionResult> AddAnnouncement([FromBody] AddAnnouncementCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            try
            {
                AnnouncementEntity eventEnt = await _mediator.Send(command);

                if (eventEnt != null)
                {
                    return Ok(eventEnt);
                }
                else
                {
                    return NotFound("L'annonce ne peut pas être ajouté");
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
            if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect id.");

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

            if (id != command.Id) return BadRequest("incorrect id.");

            try
            {
                var announcement = await _mediator.Send(command);

                if (announcement != null)
                {
                    return Ok(announcement);
                }
                else
                {
                    return NotFound("L'annonce n'a pas pu être modifié !");
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
        /// <param name="UserId">Identifiant Guid du user</param>
        /// <returns>Liste des annonces que l'utilisateur a en favoris</returns>
        [HttpGet("GetAnnouncementFavoriteByUser/{UserId}")]
        public async Task<IActionResult> GetAnnouncementFavoriteByUser(Guid UserId)
        {
            if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                GetAnnouncementFavoriteByUserCommand command = new GetAnnouncementFavoriteByUserCommand();
                command.UserId = UserId;
                List<AnnouncementDto> eventEnt = await _mediator.Send(command);


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
        /// fonction pour récupérer tous les annonces de toutes les mairies qu'un utilisateur a en favoris
        /// </summary>
        /// <param name="UserId">Identifiant Guid du user</param>
        /// <returns>Dto AnnouncementByMairieFavorite</returns>
        [HttpGet("GetAnnouncementByMairieFavorite/{UserId}")]
        public async Task<IActionResult> GetAnnouncementByMairieFavorite(Guid UserId)
        {
            if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                GetAnnouncementByMairieFavoriteCommand command = new GetAnnouncementByMairieFavoriteCommand();
                command.UserId = UserId;
                List<AnnouncementByMairieFavoriteDto> AnnouncementEnt = await _mediator.Send(command);

                if (AnnouncementEnt != null)
                {
                    return Ok(AnnouncementEnt);
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
        /// fonction pour récupérer tous les annonces d'un mairie
        /// </summary>
        /// <param name="MairieId">Identifiant Guid de la mairie</param>
        /// <returns>Liste de l'ensemble des annonces d'une mairies par ordre décroissant</returns>
        [HttpGet("GetAnnouncementHistoByMairie/{MairieId}")]
        public async Task<IActionResult> GetAnnouncementByMairie(Guid MairieId)
        {
            if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                GetAnnouncementHistoByMairieCommand command = new GetAnnouncementHistoByMairieCommand();
                command.MairieId = MairieId;
                List<AnnouncementDto> AnnouncemenetEnt = await _mediator.Send(command);

                if (AnnouncemenetEnt != null)
                {
                    return Ok(AnnouncemenetEnt);
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
        /// fonction pour récupérer les annonces d'une mairie
        /// </summary>
        /// <param name="id">Identifiant Guid de la mairie</param>
        /// <returns>Code http Ok et liste d'annonce</returns>
        [HttpGet("GetAnnouncementByMairieDetail/{UserId}/{MairieId}")]
        public async Task<IActionResult> GetAnnouncementByMairieDetail(Guid UserId, Guid MairieId)
        {
            if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect user Guid.");
            if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect mairie Guid.");

            try
            {
                GetAnnouncementByMairieDetailCommand command = new GetAnnouncementByMairieDetailCommand();
                command.UserId = UserId;
                command.MairieId = MairieId;
                List<AnnouncementByMairieDetailDto> eventEnt = await Mediator.Send(command);

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
        /// fonction pour la liste des types d'annonce
        /// </summary>
        /// <param></param>
        /// <returns>Code http Ok et liste de type d'annonce</returns>
        [HttpGet("GetAnnouncementType")]
        public async Task<IActionResult> GetAnnouncementType()
        {
            try
            {
                GetAnnouncementTypeCommand command = new GetAnnouncementTypeCommand();
                List<AnnouncementTypeEntity> AnnouncementType = await Mediator.Send(command);

                return Ok(AnnouncementType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
