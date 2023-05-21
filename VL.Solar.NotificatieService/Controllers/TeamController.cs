using Microsoft.AspNetCore.Mvc;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Repositories;

namespace VL.Solar.NotificatieService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamRepository repository;

        public TeamController(TeamRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            repository.CreateTeam(team);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = repository.GetTeams();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamById(int id)
        {
            var team = repository.GetTeamById(id);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, Team updatedTeam)
        {
            repository.UpdateTeam(id, updatedTeam);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            repository.DeleteTeam(id);
            return Ok();
        }
    }
}