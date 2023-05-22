using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;

namespace VL.Solar.NotificatieService.Repositories.Interfaces;

public interface IMedewerkerNotificatieRepository
{
    Task<List<MedewerkerNotificatie>> GetMedewerkerNotificatiesAsync();
    Task<MedewerkerNotificatie?> GetMedewerkerNotificatieByIdAsync(int medewerkerNotificatieId);
    Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByMedewerkerAsync(string medewerkerId);
    Task<IEnumerable<MedewerkerNotificatie?>> GetMedewerkerNotificatiesByNotificatieIdAsync(int notificatieId);
    Task CreateMedewerkerNotificatieAsync(CreateMedewerkerNotificatie createMedewerkerNotificatie);
    Task UpdateMedewerkerNotificatieAsync(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie);
    Task<IEnumerable<MedewerkerNotificatie?>> GetUnreadNotificatiesAsync();
}