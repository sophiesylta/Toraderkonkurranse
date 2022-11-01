using Microsoft.AspNetCore.Mvc;

using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.AngularGUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DommerController : Controller
    {
        [HttpPost]
        public async Task<Boolean> PostAsync(AddDommerDTO dommerDTO)
        {
            //'https://localhost:7134/opprettDommer?fornavn=dfg&etternavn=dfg&epost=dfg&konkurranseID=1'
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7134/");
            var result = await client.PostAsJsonAsync("Dommer/opprettDommer",dommerDTO);
            return true;
        }
    }
}
