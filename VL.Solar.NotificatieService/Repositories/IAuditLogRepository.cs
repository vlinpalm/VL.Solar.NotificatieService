using VL.Solar.NotificatieService.Models;

namespace VL.Solar.NotificatieService.Repositories;

public interface IAuditLogRepository
{
    void AddAuditLog(AuditLog auditLog);
}