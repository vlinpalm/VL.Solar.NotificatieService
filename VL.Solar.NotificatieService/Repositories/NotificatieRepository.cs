using VL.Solar.NotificatieService.Data;
using VL.Solar.NotificatieService.Models;

namespace VL.Solar.NotificatieService.Repositories
{
    public class NotificatieRepository
    {
        private readonly AppDbContext dbContext;

        public NotificatieRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CreateNotificatie(Notificatie? notificatie)
        {
            dbContext.Notificaties.Add(notificatie);
            dbContext.SaveChanges();
        }
        public List<Notificatie?> GetNotificaties()
        {
            return dbContext.Notificaties.ToList();
        }
        public Notificatie? GetNotificatieById(int notificatieId)
        {
            return dbContext.Notificaties.FirstOrDefault(n => n.NotificatieId == notificatieId);
        }
        public void UpdateNotificatie(Notificatie? notificatie)
        {
            dbContext.Notificaties.Update(notificatie);
            dbContext.SaveChanges();
        }
        public List<Notificatie?> GetNotificatiesByTeamNaam(string teamNaam)
        {
            return dbContext.Notificaties.Where(n => n.TeamNaam == teamNaam).ToList();
        }
        public void DeleteNotificatie(int notificatieId)
        {
            var notificatie = dbContext.Notificaties.FirstOrDefault(n => n != null && n.NotificatieId == notificatieId);
            if (notificatie != null)
            {
                dbContext.Notificaties.Remove(notificatie);
                dbContext.SaveChanges();
            }
        }
    }
}