using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;
using VL.Solar.NotificatieService.Repositories;
using VL.Solar.NotificatieService.Services.Interfaces;

namespace VL.Solar.NotificatieService.Services;

public class MedewerkerNotificatieService : IMedewerkerNotificatieService
{
    private readonly MedewerkerNotificatieRepository repository;

    public MedewerkerNotificatieService(MedewerkerNotificatieRepository repository)
    {
        this.repository = repository;
    }
    public List<MedewerkerNotificatie?> GetMedewerkerNotificaties()
    {
        return repository.GetMedewerkerNotificaties();
    }
    public MedewerkerNotificatie? GetMedewerkerNotificatieById(int medewerkerNotificatieId)
    {
        return repository.GetMedewerkerNotificatieById(medewerkerNotificatieId);
    }
    public IEnumerable<MedewerkerNotificatie?> GetMedewerkerNotificatiesByMedewerker(string medewerkerId)
    {
        return repository.GetMedewerkerNotificatiesByMedewerker(medewerkerId);
    }
    public IEnumerable<MedewerkerNotificatie?> GetMedewerkerNotificatiesByNotificatieId(int notificatieId)
    {
        return repository.GetMedewerkerNotificatiesByNotificatieId(notificatieId);
    }
    public void CreateMedewerkerNotificatie(CreateMedewerkerNotificatie createMedewerkerNotificatie)
    {
        repository.CreateMedewerkerNotificatie(createMedewerkerNotificatie);
    }
    public void UpdateMedewerkerNotificatie(int medewerkerNotificatieId, MedewerkerNotificatie updatedMedewerkerNotificatie)
    {
        var medewerkerNotificatie = repository.GetMedewerkerNotificatieById(medewerkerNotificatieId);
        if (medewerkerNotificatie != null)
        {
            medewerkerNotificatie.Gelezen = updatedMedewerkerNotificatie.Gelezen;
            repository.UpdateMedewerkerNotificatie(medewerkerNotificatieId, updatedMedewerkerNotificatie);
        }
    }
    public IEnumerable<MedewerkerNotificatie?> GetUnreadNotificaties()
    {   
        return repository.GetUnreadNotificaties();
    }
}
