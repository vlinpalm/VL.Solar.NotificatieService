using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;

namespace VL.Solar.NotificatieService.Services.Interfaces;

public interface IMedewerkerNotificatieService
{
    List<MedewerkerNotificatie?> GetMedewerkerNotificaties();
    MedewerkerNotificatie? GetMedewerkerNotificatieById(int medewerkerNotificatieId);
    IEnumerable<MedewerkerNotificatie?> GetMedewerkerNotificatiesByMedewerker(string medewerkerId);
    IEnumerable<MedewerkerNotificatie?> GetMedewerkerNotificatiesByNotificatieId(int notificatieId);
    void CreateMedewerkerNotificatie(CreateMedewerkerNotificatie createMedewerkerNotificatie);
    IEnumerable<MedewerkerNotificatie?> GetUnreadNotificaties();
    void UpdateMedewerkerNotificatie(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie);
}
