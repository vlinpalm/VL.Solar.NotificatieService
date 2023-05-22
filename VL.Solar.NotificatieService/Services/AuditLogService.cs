using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Repositories;
using VL.Solar.NotificatieService.Repositories.Interfaces;
using VL.Solar.NotificatieService.Services.Interfaces;

namespace VL.Solar.NotificatieService.Services
{
    public class AuditLogService : IAuditLogService

    {
        private readonly IAuditLogRepository auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            this.auditLogRepository = auditLogRepository;
        }


    public void LogAuditEvent(string action, string medewerkerId)
    {
        var auditLog = new AuditLog
        {
            Actie = action,
            MedewerkerId = medewerkerId,
            Timestamp = DateTime.UtcNow
        };

        auditLogRepository.AddAuditLog(auditLog);
    }
    }
}