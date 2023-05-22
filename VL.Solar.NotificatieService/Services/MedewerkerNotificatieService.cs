using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;
using VL.Solar.NotificatieService.Services.Interfaces;
using VL.Solar.NotificatieService.Repositories.Interfaces;

namespace VL.Solar.NotificatieService.Services
{
    public class MedewerkerNotificatieService : IMedewerkerNotificatieService
    {
        private readonly IMedewerkerNotificatieRepository repository;

        public MedewerkerNotificatieService(IMedewerkerNotificatieRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<MedewerkerNotificatie>> GetMedewerkerNotificatiesAsync()
        {
            return await repository.GetMedewerkerNotificatiesAsync();
        }

        public async Task<MedewerkerNotificatie?> GetMedewerkerNotificatieByIdAsync(int medewerkerNotificatieId)
        {
            return await repository.GetMedewerkerNotificatieByIdAsync(medewerkerNotificatieId);
        }

        public async Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByMedewerkerAsync(string medewerkerId)
        {
            return await repository.GetMedewerkerNotificatiesByMedewerkerAsync(medewerkerId);
        }

        public async Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByNotificatieIdAsync(int notificatieId)
        {
            return await repository.GetMedewerkerNotificatiesByNotificatieIdAsync(notificatieId);
        }

        public async Task CreateMedewerkerNotificatieAsync(CreateMedewerkerNotificatie createMedewerkerNotificatie)
        {
            await repository.CreateMedewerkerNotificatieAsync(createMedewerkerNotificatie);
        }

        public async Task UpdateMedewerkerNotificatieAsync(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie)
        {
            var medewerkerNotificatie = await repository.GetMedewerkerNotificatieByIdAsync(medewerkerNotificatieId);
            if (medewerkerNotificatie != null)
            {
                medewerkerNotificatie.Gelezen = updatedMedewerkerNotificatie.Gelezen;
                await repository.UpdateMedewerkerNotificatieAsync(medewerkerNotificatieId, updatedMedewerkerNotificatie);
            }
        }

        public async Task<IEnumerable<MedewerkerNotificatie?>> GetUnreadNotificatiesAsync()
        {
            return await repository.GetUnreadNotificatiesAsync();
        }
    }
}
