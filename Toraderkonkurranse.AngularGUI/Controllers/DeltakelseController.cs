using Microsoft.AspNetCore.Mvc;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.AngularGUI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DeltakelseController : ControllerBase
    {
        // kan importere DTO fra API, eller lage en klasser som tilsvarer objektet på samme mappenivå som controllerene i GUI-prosjekt
        [HttpGet]
        public async Task<IEnumerable<GetArrangementDTO>> GetAsync()
        {
            HttpClient client = new HttpClient();
            //Adresse fra swagger
            client.BaseAddress = new Uri("https://localhost:7134/");
            // "Deltakelse/getAlleArrangement" - må stemme overens med swagger
            List<GetArrangementDTO> arrangement = await client.GetFromJsonAsync<List<GetArrangementDTO>>("Deltakelse/getAlleArrangement");
            return arrangement;

            //return new List<GetArrangementDTO>()
            //{
            //    new GetArrangementDTO()
            //    {
            //        arrangor= "sophie",
            //        navn = "vm",
            //        lokasjon = "osterøy"

            //    }
            //};
        }
        [HttpPost]
        public async Task<Boolean> PostAsync(int arrangementID, int konkurranseID, AddDeltakerDTO deltaker)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7134/");
            var result = await client.PostAsJsonAsync<AddDeltakerDTO>("Deltakelse/meldPaaDeltaker?arrangementID=1?konkurranseID=1" ,deltaker);
            return true;
        }
    }
    
}
