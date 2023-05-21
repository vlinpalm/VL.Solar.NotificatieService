using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VL.Solar.NotificatieService.Services.Interfaces;

public class AuditLogActionFilter : IAsyncActionFilter
{
    string currentUser = "User123";
    private readonly IAuditLogService auditLogService;

    public AuditLogActionFilter(IAuditLogService auditLogService)
    {
        this.auditLogService = auditLogService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        auditLogService.LogAuditEvent(actionName, currentUser);
        await next();
    }
}