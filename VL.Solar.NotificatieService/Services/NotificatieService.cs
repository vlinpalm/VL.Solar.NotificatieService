using RabbitMQ.Client;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Repositories;
using VL.Solar.NotificatieService.Services.Interfaces;

namespace VL.Solar.NotificatieService.Services;

public class NotificatieService : INotificatieService
{
    private readonly NotificatieRepository repository;
    private readonly RabbitMQService rabbitMQService;

    public NotificatieService(NotificatieRepository repository, RabbitMQService rabbitMQService)
    {
        this.repository = repository;
        this.rabbitMQService = rabbitMQService;
    }
    public void CreateNotificatie(Notificatie? notificatie)
    {
        // Validatie van invoergegevens
        var validationErrors = ValidateNotificatie(notificatie);
        if (validationErrors.Any())
        {
            throw new ArgumentException(string.Join(Environment.NewLine, validationErrors));
        }
        repository.CreateNotificatie(notificatie);

        rabbitMQService.SendMessageToRabbitMQ(notificatie);
    }
    public List<Notificatie?> GetNotificaties()
    {
        return repository.GetNotificaties();
    }

    public Notificatie? GetNotificatieById(int notificatieId)
    {
        return repository.GetNotificatieById(notificatieId);
    }

    public void UpdateNotificatie(Notificatie? notificatie)
    {
        repository.UpdateNotificatie(notificatie);
    }

    public List<Notificatie?> GetNotificatiesByTeamNaam(string teamNaam)
    {
        return repository.GetNotificatiesByTeamNaam(teamNaam);
    }

    public void DeleteNotificatie(int notificatieId)
    {
        repository.DeleteNotificatie(notificatieId);
    }
    private List<string> ValidateNotificatie(Notificatie notificatie)
    {
        var validationErrors = new List<string>();

        if (string.IsNullOrEmpty(notificatie.Inhoud))
        {
            validationErrors.Add("Inhoud is vereist.");
        }
        if (string.IsNullOrEmpty(notificatie.Bron))
        {
            validationErrors.Add("Bron is vereist.");
        }

        if (notificatie.DossierNummer <= 0)
        {
            validationErrors.Add("Ongeldig DossierNummer. Het moet een positief getal zijn.");
        }
        return validationErrors;
    }
}
