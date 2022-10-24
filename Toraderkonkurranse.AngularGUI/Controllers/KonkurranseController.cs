using Microsoft.AspNetCore.Mvc;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.AngularGUI.Controllers
{
    //Dette må være med! ([ApiController] og [Route("[controller]")])
    [ApiController]
    [Route("[controller]")]
    public class KonkurranseController : Controller
    {
        public async Task<IEnumerable<Konkurranse>> GetAsync(int arrangementID)
        {
            HttpClient client = new HttpClient();
            //Adresse fra swagger
            client.BaseAddress = new Uri("https://localhost:7134/");
            // Deltakelse/getKonkurranseIArrangement?arrangementID=1 - henter konkurranser fra arrangement med ID = 1
            List<Konkurranse> konkurranser = await client.GetFromJsonAsync<List<Konkurranse>>("Deltakelse/getKonkurranseIArrangement?arrangementID=" + arrangementID);
            return konkurranser;
        }

        //Test uten kobling til API
        //public IEnumerable<Konkurranse> Get()
        //{
        //    return new List<Konkurranse>()
        //    {
        //        new Konkurranse(){ navn = "Solist", maxAntallDeltakere = 1}
        //    };
        //}
    }
}
