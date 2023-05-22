using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;


namespace VL.Solar.NotificatieService.Services.Interfaces
{
    public interface IMedewerkerNotificatieService
    {
        Task<List<MedewerkerNotificatie>> GetMedewerkerNotificatiesAsync();
        Task<MedewerkerNotificatie?> GetMedewerkerNotificatieByIdAsync(int medewerkerNotificatieId);
        Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByMedewerkerAsync(string medewerkerId);
        Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByNotificatieIdAsync(int notificatieId);
        Task CreateMedewerkerNotificatieAsync(CreateMedewerkerNotificatie createMedewerkerNotificatie);
        Task<IEnumerable<MedewerkerNotificatie?>> GetUnreadNotificatiesAsync();
        Task UpdateMedewerkerNotificatieAsync(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie);
    }
}