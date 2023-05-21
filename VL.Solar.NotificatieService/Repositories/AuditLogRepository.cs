using VL.Solar.NotificatieService.Data;
using VL.Solar.NotificatieService.Models;

namespace VL.Solar.NotificatieService.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext dbContext;

        public AuditLogRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddAuditLog(AuditLog auditLog)
        {
            dbContext.AuditLogs.Add(auditLog);
            dbContext.SaveChanges();
        }
    }
}