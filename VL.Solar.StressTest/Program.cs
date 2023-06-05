using System.Text;
using NBomber.Contracts;
using NBomber.CSharp;
using Newtonsoft.Json;

namespace VL.Solar.StressTest;

public class Program
{
    public static void Main(string[] args)
    {
        var step = Step.Create("POST request", async context =>
        {
            var client = new HttpClient();

            var notification = new Notificatie
            {
                BerichtType = "Informatie",
                Inhoud = "{'a': 'Notification Message'}",
                Bron = "Bron1",
                DossierNummer = 12345
            };

            var json = JsonConvert.SerializeObject(notification);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://api.yoursite.com/notifications", data);

            return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
        });

        var scenario = ScenarioBuilder
            .CreateScenario("POST load test", step)
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 300, during: TimeSpan.FromMinutes(1))
            );

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();
    }
}

public class Notificatie
{
    public int NotificatieId { get; set; }
    public string BerichtType { get; set; }
    public string Bron { get; set; }
    public string TeamNaam { get; set; }
    public string Inhoud { get; set; }
    public int DossierNummer { get; set; }
    public DateTime MutatieDatum { get; set; }
    public DateTime DatumVerwerkt { get; set; }
}