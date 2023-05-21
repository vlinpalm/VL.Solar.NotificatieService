using VL.Solar.NotificatieService.Models;

namespace VL.Solar.NotificatieService.Services.Interfaces;

public interface INotificatieService
{
    void CreateNotificatie(Notificatie? notificatie);
    List<Notificatie?> GetNotificaties();
    Notificatie? GetNotificatieById(int notificatieId);
    void UpdateNotificatie(Notificatie? notificatie);
    List<Notificatie?> GetNotificatiesByTeamNaam(string teamNaam);
    void DeleteNotificatie(int notificatieId);
}
