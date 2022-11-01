using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.AngularGUI.Controllers
{
    //Dette må være med! ([ApiController] og [Route("[controller]")])
    [ApiController]
    [Route("[controller]")]
    public class ResultatController : Controller
    {
        public async Task<string> GetAsync(int konkurranseID)
        {
            HttpClient client = new HttpClient();
            //Adresse fra swagger
            client.BaseAddress = new Uri("https://localhost:7134/");
            var resultatListe = await client.GetStringAsync("Arrangement/getResultatliste?konkurranseID=" + konkurranseID);
            return resultatListe;
        }

    }
}
