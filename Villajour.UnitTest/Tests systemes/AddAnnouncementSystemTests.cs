using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Villajour.Domain.Common;
using Xunit;

[Binding]
public class AddAnnouncementSteps
{
    private readonly HttpClient _client;
    private HttpResponseMessage _response;
    private string _title;
    private string _description;

    public AddAnnouncementSteps()
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Given(@"une annonce avec les informations suivantes")]
    public void GivenUneAnnonceAvecLesInformationsSuivantes(TechTalk.SpecFlow.Table table)
    {
        _title = table.Rows[0]["Titre"];
        _description = table.Rows[0]["Description"];
    }

    [When(@"l'utilisateur ajoute l'annonce")]
    public async Task WhenLUtilisateurAjouteLAnnonce()
    {
        var announcementData = new { Title = _title, Description = _description };
        var content = new StringContent(JsonConvert.SerializeObject(announcementData), Encoding.UTF8, "application/json");

        _response = await _client.PostAsync("/api/announcement", content);
    }

    [Then(@"la réponse doit être ""(.*)""")]
    public void ThenLaReponseDoitEtre(string expectedStatusCode)
    {
        var actualStatusCode = _response.StatusCode.ToString();
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }

    [Then(@"l'annonce doit contenir ""(.*)"" et ""(.*)""")]
    public async Task ThenLAnnonceDoitContenirEt(string expectedTitle, string expectedDescription)
    {
        var responseData = await _response.Content.ReadAsStringAsync();
        var announcement = JsonConvert.DeserializeObject<AnnouncementEntity>(responseData);

        Assert.Equal(expectedTitle, announcement.Title);
        Assert.Equal(expectedDescription, announcement.Description);
    }
}
