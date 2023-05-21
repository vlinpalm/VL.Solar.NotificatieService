using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Services.Interfaces;

namespace VL.Solar.NotificatieService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ServiceFilter(typeof(AuditLogActionFilter))]
    public class NotificatieController : ControllerBase
    {
        private readonly DbContext dbContext;
        private readonly INotificatieService notificatieService;

        public NotificatieController(INotificatieService notificatieService)
        {
            this.notificatieService = notificatieService;
        }
        [HttpPost]
        public IActionResult CreateNotificatie(Notificatie notificatie)
        {
            try
            {
                notificatieService.CreateNotificatie(notificatie);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetNotificaties()
        {
            var notificaties = notificatieService.GetNotificaties();
            return Ok(notificaties);
        }
        [HttpGet("{id}")]
        public IActionResult GetNotificatieById(int id)
        {
            var notificatie = notificatieService.GetNotificatieById(id);
            if (notificatie == null)
                return NotFound();

            return Ok(notificatie);
        }
        [HttpGet("{teamNaam}")]
        public IActionResult GetNotificatiesByTeamNaam(string teamNaam)
        {
            var notificaties = notificatieService.GetNotificatiesByTeamNaam(teamNaam);
            return Ok(notificaties);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateNotificatie(int id, Notificatie notificatie)
        {
            var existingNotificatie = notificatieService.GetNotificatieById(id);
            if (existingNotificatie == null)
                return NotFound();

            existingNotificatie.BerichtType = notificatie.BerichtType;
            existingNotificatie.Bron = notificatie.Bron;
            existingNotificatie.TeamNaam = notificatie.TeamNaam;
            existingNotificatie.Inhoud = notificatie.Inhoud;
            existingNotificatie.DossierNummer = notificatie.DossierNummer;
            existingNotificatie.MutatieDatum = notificatie.MutatieDatum;
            existingNotificatie.DatumVerwerkt = notificatie.DatumVerwerkt;
            
            notificatieService.UpdateNotificatie(existingNotificatie);

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNotificatie(int id)
        {
            var existingNotificatie = notificatieService.GetNotificatieById(id);
            if (existingNotificatie == null)
                return NotFound();

            notificatieService.DeleteNotificatie(id);
            return Ok();
        }
    }
}
