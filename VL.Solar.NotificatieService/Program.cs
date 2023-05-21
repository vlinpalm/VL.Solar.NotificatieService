using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VL.Solar.NotificatieService.Data;
using VL.Solar.NotificatieService.Helpers;
using VL.Solar.NotificatieService.Repositories;
using VL.Solar.NotificatieService.Services;
using VL.Solar.NotificatieService.Services.Interfaces;


var appBuilder = WebApplication.CreateBuilder(args);
appBuilder.Services.AddControllers();
appBuilder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
appBuilder.Services.AddScoped<IAuditLogService, AuditLogService>();
appBuilder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(appBuilder.Configuration.GetConnectionString("DefaultConnection")));
appBuilder.Services.AddAutoMapper(typeof(AutoMapperProfile));
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddSwaggerGen();

var app = appBuilder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors(corsBuilder =>
{
    if (app.Environment.IsDevelopment())
    {
        corsBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    }
    else
    {
        corsBuilder.WithOrigins("https://*.ourinternalhosting.net")
            .AllowAnyMethod()
            .AllowAnyHeader();
    }
});
app.MapControllers();

app.Run();