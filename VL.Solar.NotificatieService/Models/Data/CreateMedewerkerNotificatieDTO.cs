namespace VL.Solar.NotificatieService.Models.Data;

public class CreateMedewerkerNotificatie
{
    public int NotificatieId { get; set; }
    public string MedewerkerId { get; set; }
    public bool Gelezen { get; set; }
    public DateTime MutatieDatum { get; set; }
    public DateTime NotificatieDatum { get; set; }

}