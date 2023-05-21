using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;
using VL.Solar.NotificatieService.Services.Interfaces;

namespace VL.Solar.NotificatieService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(AuditLogActionFilter))]
    public class MedewerkerNotificatieController : ControllerBase
    {
        private readonly IMedewerkerNotificatieService medewerkerNotificatieService;

        public MedewerkerNotificatieController(IMedewerkerNotificatieService medewerkerNotificatieService)
        {
            this.medewerkerNotificatieService = medewerkerNotificatieService;
        }

        [HttpPost]
        public IActionResult CreateMedewerkerNotificatie(CreateMedewerkerNotificatie createMedewerkerNotificatie)
        {
            medewerkerNotificatieService.CreateMedewerkerNotificatie(createMedewerkerNotificatie);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetMedewerkerNotificaties()
        {
            var medewerkerNotificaties = medewerkerNotificatieService.GetMedewerkerNotificaties();
            return Ok(medewerkerNotificaties);
        }

        [HttpGet("{id}")]
        public IActionResult GetMedewerkerNotificatieById(int id)
        {
            var medewerkerNotificatie = medewerkerNotificatieService.GetMedewerkerNotificatieById(id);
            if (medewerkerNotificatie == null)
                return NotFound();

            return Ok(medewerkerNotificatie);
        }
        
        [HttpGet("{notificatieId}")]
        public IActionResult GetMedewerkerNotificatiesByNotificatieId(int notificatieId)
        {
            var medewerkerNotificaties = medewerkerNotificatieService.GetMedewerkerNotificatiesByNotificatieId(notificatieId);
            return Ok(medewerkerNotificaties);
        }

        [HttpGet("{medewerkerId}")]
        public IActionResult GetMedewerkerNotificatieByMedewerkerId(string medewerkerId)
        {
            var medewerkerNotificaties = medewerkerNotificatieService.GetMedewerkerNotificatiesByMedewerker(medewerkerId);
            return Ok(medewerkerNotificaties);
        }
        
        [HttpGet("unread")]
        public IActionResult GetUnreadNotificaties()
        {
            var unreadNotificaties = medewerkerNotificatieService.GetUnreadNotificaties();
            return Ok(unreadNotificaties);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMedewerkerNotificatie(int id, MedewerkerNotificatie updatedNotification)
        {
            var existingNotification = medewerkerNotificatieService.GetMedewerkerNotificatieById(id);
            if (existingNotification == null)
                return NotFound();
            
            existingNotification.Gelezen = updatedNotification.Gelezen;

            medewerkerNotificatieService.UpdateMedewerkerNotificatie(id, existingNotification);
            return Ok();
        }

    }
}
