using AutoMapper;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;
using VL.Solar.NotificatieService.Data;

namespace VL.Solar.NotificatieService.Repositories
{
    public class MedewerkerNotificatieRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public MedewerkerNotificatieRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<MedewerkerNotificatie> GetMedewerkerNotificaties()
        {
            return dbContext.MedewerkerNotificaties.ToList();
        }

        public MedewerkerNotificatie? GetMedewerkerNotificatieById(int medewerkerNotificatieId)
        {
            return dbContext.MedewerkerNotificaties.FirstOrDefault(mNotificatie =>
                mNotificatie.MedewerkerNotificationId == medewerkerNotificatieId);
        }

        public IEnumerable<MedewerkerNotificatie?> GetMedewerkerNotificatiesByMedewerker(string medewerkerId)
        {
            return dbContext.MedewerkerNotificaties.Where(mNotificatie => mNotificatie.MedewerkerId == medewerkerId);
        }
        public IEnumerable<MedewerkerNotificatie?> GetMedewerkerNotificatiesByNotificatieId(int notificatieId)
        {
            return dbContext.MedewerkerNotificaties.Where(mNotificatie => mNotificatie.NotificatieId == notificatieId);
        }
        public void CreateMedewerkerNotificatie(CreateMedewerkerNotificatie createMedewerkerNotificatie)
        {
            var medewerkerNotificatie = mapper.Map<MedewerkerNotificatie>(createMedewerkerNotificatie);
            dbContext.MedewerkerNotificaties.Add(medewerkerNotificatie);
            dbContext.SaveChanges();
        }
        public void UpdateMedewerkerNotificatie(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie)
        {
            var chosenMedewerkerNotificatie = dbContext.MedewerkerNotificaties.FirstOrDefault(mNotificatie =>
                mNotificatie.MedewerkerNotificationId == medewerkerNotificatieId);

            if (chosenMedewerkerNotificatie != null)
            {
                chosenMedewerkerNotificatie.Gelezen = updatedMedewerkerNotificatie.Gelezen ;
                dbContext.SaveChanges();
            }
        }
        
        public IEnumerable<MedewerkerNotificatie?> GetUnreadNotificaties()
        {
            return dbContext.MedewerkerNotificaties.Where(mNotificatie => mNotificatie.Gelezen == false);
        }
    }

}
