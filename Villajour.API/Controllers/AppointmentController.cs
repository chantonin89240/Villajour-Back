using Microsoft.AspNetCore.Mvc;
using Villajour.Domain.Common;
using MediatR;
using Villajour.Application.Commands.Appointments.AddAppointment;
using Microsoft.IdentityModel.Tokens;
using Villajour.Application.Commands.Appointments.GetAppointmentByUser;
using Villajour.Application.Commands.Appointments.GetAppointmentByMairie;
using Villajour.Application.Commands.Appointments.ValidateAppointment;
using Villajour.Application.Commands.Appointments.DeleteAppointment;

namespace Villajour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
        _mediator = mediator;

        }

        /// <summary>
        /// fonction pour récupérer les rdv d'un user
        /// </summary>
        /// <param name="UserId">Identifiant Guid du user</param>
        /// <returns>Liste des rdv de l'utilisateur
        [HttpGet("GetAppointmentByUser/{UserId}")]
        public async Task<IActionResult> GetAppointmentByUser(Guid UserId)
        {
            if (UserId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                GetAppointmentByUserCommand command = new GetAppointmentByUserCommand();
                command.UserId = UserId;
                List<AppointmentEntity> appointmentEnt = await _mediator.Send(command);

                return Ok(appointmentEnt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// fonction pour récupérer tous les rdv d'un mairie
        /// </summary>
        /// <param name="MairieId">Identifiant Guid de la mairie</param>
        /// <returns>Liste de l'ensemble des rdv d'une mairies par ordre décroissant</returns>
        [HttpGet("GetAppointmentByMairie/{MairieId}")]
        public async Task<IActionResult> GetAppointmentByMairie(Guid MairieId)
        {
            if (MairieId.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                GetAppointmentByMairieCommand command = new GetAppointmentByMairieCommand();
                command.MairieId = MairieId;
                List<AppointmentEntity> appointmentEnt = await _mediator.Send(command);

                return Ok(appointmentEnt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// fonction pour l'ajout d'un rdv
        /// </summary>
        /// <param name="command">Propriété de la command</param>
        /// <returns>code http Ok avec l'entité Appointment</returns>
        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] AddAppointmentCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            try
            {
                AppointmentEntity appointmentEnt = await _mediator.Send(command);

                if (appointmentEnt != null)
                {
                    return Ok(appointmentEnt);
                }
                else
                {
                    return NotFound("Le rendez-vous ne peut pas être ajouté");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// fonction pour supprimer un rdv
        /// </summary>
        /// <param name="id">Identifiant id du rdv</param>
        /// <returns>Code http</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (id.ToString().IsNullOrEmpty()) return BadRequest("incorrect Guid.");

            try
            {
                DeleteAppointmentCommand command = new DeleteAppointmentCommand();
                command.Id = id;
                bool AppointmentEnt = await _mediator.Send(command);

                if (AppointmentEnt)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(400, "Le rdv n'existe pas !");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// fonction pour la modification d'un rdv
        /// </summary>
        /// <param name="id">Id de rdv</param>
        /// <param name="command">Propriété de la command</param>
        /// <returns>Code http Ok avec l'entité rdv</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            if (id != command.Id) return BadRequest("incorrect Guid.");

            try
            {
                var appointmentEnt = await _mediator.Send(command);

                if (appointmentEnt != null)
                {
                    return Ok(appointmentEnt);
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
