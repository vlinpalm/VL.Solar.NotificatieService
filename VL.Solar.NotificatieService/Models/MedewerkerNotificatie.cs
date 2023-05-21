namespace VL.Solar.NotificatieService.Models;

public class MedewerkerNotificatie
{
    public int MedewerkerNotificationId { get; set; }
    public int NotificatieId { get; set; }
    public string MedewerkerId { get; set; }
    public bool Gelezen { get; set; }
    public virtual Notificatie Notificatie { get; set; }
    public DateTime MutatieDatum { get; set; }
    public DateTime NotificatieDatum { get; set; }

}