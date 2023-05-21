using VL.Solar.NotificatieService.Models;

namespace VL.Solar.NotificatieService.Repositories
{
    public class TeamRepository
    {
        private readonly List<Team> teams;

        public TeamRepository()
        {
            teams = new List<Team>();
        }

        public void CreateTeam(Team team)
        {
            teams.Add(team);
        }

        public List<Team> GetTeams()
        {
            return teams;
        }

        public Team GetTeamById(int teamId)
        {
            return teams.FirstOrDefault(t => t.TeamId == teamId);
        }
        
        public void UpdateTeam(int teamId, Team updatedTeam)
        {
            var team = teams.FirstOrDefault(t => t.TeamId == teamId);
            
            if (team != null)
            {
                team.TeamEmailAdres = updatedTeam.TeamEmailAdres;
                team.TeamNaam = updatedTeam.TeamNaam;
            }
        }
        
        public void DeleteTeam(int teamId)
        {
            var team = teams.FirstOrDefault(t => t.TeamId == teamId);
            
            if (team != null)
            {
                teams.Remove(team);
            }
        }
    }
}