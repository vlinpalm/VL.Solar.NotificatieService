namespace VL.Solar.NotificatieService.Services.Interfaces;

public interface IAuditLogService
{
    void LogAuditEvent(string action, string medewerkerId = null!);
}
