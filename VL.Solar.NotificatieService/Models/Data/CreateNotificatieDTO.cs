namespace VL.Solar.NotificatieService.Models.Data;

public class CreateNotificatieDTO
{
    public string BerichtType { get; set; }
    public string Bron { get; set; }
    public string Inhoud { get; set; }
    public int DossierNummer { get; set; }
    public DateTime MutatieDatum { get; set; }
    public DateTime DatumVerwerkt { get; set; }

}