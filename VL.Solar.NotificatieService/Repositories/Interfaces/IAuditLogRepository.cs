using VL.Solar.NotificatieService.Models;

namespace VL.Solar.NotificatieService.Repositories.Interfaces;

public interface IAuditLogRepository
{
    void AddAuditLog(AuditLog auditLog);
}