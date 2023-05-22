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
        private readonly IMedewerkerNotificatieService _medewerkerNotificatieService;

        public MedewerkerNotificatieController(IMedewerkerNotificatieService medewerkerNotificatieService)
        {
            _medewerkerNotificatieService = medewerkerNotificatieService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedewerkerNotificatie(CreateMedewerkerNotificatie createMedewerkerNotificatie)
        {
            await _medewerkerNotificatieService.CreateMedewerkerNotificatieAsync(createMedewerkerNotificatie);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMedewerkerNotificaties()
        {
            var medewerkerNotificaties = await _medewerkerNotificatieService.GetMedewerkerNotificatiesAsync();
            return Ok(medewerkerNotificaties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedewerkerNotificatieById(int id)
        {
            var medewerkerNotificatie = await _medewerkerNotificatieService.GetMedewerkerNotificatieByIdAsync(id);
            if (medewerkerNotificatie == null)
                return NotFound();

            return Ok(medewerkerNotificatie);
        }
        
        [HttpGet("{notificatieId}")]
        public async Task<IActionResult> GetMedewerkerNotificatiesByNotificatieId(int notificatieId)
        {
            var medewerkerNotificaties = await _medewerkerNotificatieService.GetMedewerkerNotificatiesByNotificatieIdAsync(notificatieId);
            return Ok(medewerkerNotificaties);
        }

        [HttpGet("{medewerkerId}")]
        public async Task<IActionResult> GetMedewerkerNotificatieByMedewerkerId(string medewerkerId)
        {
            var medewerkerNotificaties = await _medewerkerNotificatieService.GetMedewerkerNotificatiesByMedewerkerAsync(medewerkerId);
            return Ok(medewerkerNotificaties);
        }
        
        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadNotificaties()
        {
            var unreadNotificaties = await _medewerkerNotificatieService.GetMedewerkerNotificatiesAsync();
            return Ok(unreadNotificaties);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedewerkerNotificatie(int id, MedewerkerNotificatie updatedNotification)
        {
            var existingNotification = await _medewerkerNotificatieService.GetMedewerkerNotificatieByIdAsync(id);
            if (existingNotification == null)
                return NotFound();
            
            existingNotification.Gelezen = updatedNotification.Gelezen;

            await _medewerkerNotificatieService.UpdateMedewerkerNotificatieAsync(id, existingNotification);
            return Ok();
        }

    }
}
