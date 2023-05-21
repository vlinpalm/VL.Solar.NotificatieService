namespace VL.Solar.NotificatieService.Models;

public class AuditLog
{
    public int Id { get; set; }
    public string Actie { get; set; }
    public string MedewerkerId { get; set; }
    public DateTime Timestamp { get; set; }
}