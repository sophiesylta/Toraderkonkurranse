using Microsoft.AspNetCore.Mvc;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.AngularGUI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ArrangementController : Controller
    {

        [HttpPost]
        public async Task<Boolean> PostAsync(AddArrangementDTO arrangement)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7134/");
            var result = await client.PostAsJsonAsync("Arrangement/OpprettArrangement", arrangement);
            return true;
        }
    }
}

