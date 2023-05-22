using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;
using VL.Solar.NotificatieService.Data;
using VL.Solar.NotificatieService.Repositories.Interfaces;

namespace VL.Solar.NotificatieService.Repositories
{
    public class MedewerkerNotificatieRepository : IMedewerkerNotificatieRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public MedewerkerNotificatieRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<MedewerkerNotificatie>> GetMedewerkerNotificatiesAsync()
        {
            return await dbContext.MedewerkerNotificaties.ToListAsync();
        }

        public async Task<MedewerkerNotificatie?> GetMedewerkerNotificatieByIdAsync(int medewerkerNotificatieId)
        {
            return await dbContext.MedewerkerNotificaties.FirstOrDefaultAsync(mNotificatie =>
                mNotificatie.MedewerkerNotificationId == medewerkerNotificatieId);
        }

        public async Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByMedewerkerAsync(string medewerkerId)
        {
            return await dbContext.MedewerkerNotificaties.Where(mNotificatie => mNotificatie.MedewerkerId == medewerkerId).ToListAsync();
        }

        public async Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByNotificatieIdAsync(int notificatieId)
        {
            return await dbContext.MedewerkerNotificaties.Where(mNotificatie => mNotificatie.NotificatieId == notificatieId).ToListAsync();
        }

        public async Task CreateMedewerkerNotificatieAsync(CreateMedewerkerNotificatie createMedewerkerNotificatie)
        {
            var medewerkerNotificatie = mapper.Map<MedewerkerNotificatie>(createMedewerkerNotificatie);
            dbContext.MedewerkerNotificaties.Add(medewerkerNotificatie);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateMedewerkerNotificatieAsync(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie)
        {
            var chosenMedewerkerNotificatie = await dbContext.MedewerkerNotificaties.FirstOrDefaultAsync(mNotificatie =>
                mNotificatie.MedewerkerNotificationId == medewerkerNotificatieId);

            if (chosenMedewerkerNotificatie != null)
            {
                chosenMedewerkerNotificatie.Gelezen = updatedMedewerkerNotificatie.Gelezen;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MedewerkerNotificatie?>> GetUnreadNotificatiesAsync()
        {
            return await dbContext.MedewerkerNotificaties.Where(mNotificatie => mNotificatie.Gelezen == false).ToListAsync();
        }
    }
}
