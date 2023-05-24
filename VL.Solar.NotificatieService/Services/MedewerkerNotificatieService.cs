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
            var validationErrors = ValidateCreateMedewerkerNotificatie(createMedewerkerNotificatie);
            if (validationErrors.Count > 0)
            {
                throw new ArgumentException(string.Join(Environment.NewLine, validationErrors));
            }

            await repository.CreateMedewerkerNotificatieAsync(createMedewerkerNotificatie);
        }

        public async Task UpdateMedewerkerNotificatieAsync(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie)
        {
            var medewerkerNotificatie = await repository.GetMedewerkerNotificatieByIdAsync(medewerkerNotificatieId);
            if (medewerkerNotificatie == null)
            {
                throw new ArgumentException($"MedewerkerNotificatie with ID {medewerkerNotificatieId} not found.");
            }

            var validationErrors = ValidateMedewerkerNotificatie(updatedMedewerkerNotificatie);
            if (validationErrors.Count > 0)
            {
                throw new ArgumentException(string.Join(Environment.NewLine, validationErrors));
            }

            medewerkerNotificatie.Gelezen = updatedMedewerkerNotificatie.Gelezen;
            await repository.UpdateMedewerkerNotificatieAsync(medewerkerNotificatieId, updatedMedewerkerNotificatie);
        }

        public async Task<IEnumerable<MedewerkerNotificatie?>> GetUnreadNotificatiesAsync()
        {
            return await repository.GetUnreadNotificatiesAsync();
        }

        private List<string> ValidateCreateMedewerkerNotificatie(CreateMedewerkerNotificatie createMedewerkerNotificatie)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrEmpty(createMedewerkerNotificatie.MedewerkerId))
            {
                validationErrors.Add("MedewerkerId is verplicht.");
            }

            if (createMedewerkerNotificatie.NotificatieId <= 0)
            {
                validationErrors.Add("NotificatieId bestaat niet.");
            }

            return validationErrors;
        }

        private List<string> ValidateMedewerkerNotificatie(MedewerkerNotificatie medewerkerNotificatie)
        {
            var validationErrors = new List<string>();

            if (medewerkerNotificatie == null)
            {
                validationErrors.Add("MedewerkerNotificatie is verplicht.");
                return validationErrors;
            }


            return validationErrors;
        }
    }
}
