using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.Infrastructure.Persistence.Context;

namespace Toraderkonkurranse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeltakerController : ControllerBase
    {
        [HttpGet("getDeltakere")]
        public List<Deltaker> deltakerListe([FromServices] ToraderkonkurranseContext toraderDbContext)
        {
            return toraderDbContext.Deltakere.Include(e => e.personer).ToList();
        }
    }
}
