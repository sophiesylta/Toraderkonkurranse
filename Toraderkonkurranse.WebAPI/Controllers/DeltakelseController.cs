using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toraderkonkurranse.Application;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.Infrastructure.Persistence.Context;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeltakelseController : ControllerBase
    {
        private readonly IDeltakerService deltakerService;

        public DeltakelseController(IDeltakerService deltakerService)
        {
            this.deltakerService = deltakerService;
        }
        [HttpGet("getDeltakelse")]
        public List<Deltakelse> deltakelseListe([FromServices] ToraderkonkurranseContext toraderDbContext)
        {
            return toraderDbContext.Deltakelse.ToList();
        }

        [HttpPost("meldPaaDeltaker")]
        public void meldPaaDeltaker(AddDeltakerDTO deltakerDTO)
        {
            deltakerService.meldPaaDeltaker(deltakerDTO.arrangementID, deltakerDTO.konkurranseID, deltakerDTO);
        }

        [HttpGet("getAlleArrangement")]
        public IEnumerable<GetArrangementDTO> getAlleArrangement()
        {
            return deltakerService.getAlleArrangement();
        }

        [HttpGet("getKonkurranseIArrangement")]
        public List<Konkurranse> getKonkurranseIArrangement(int arrangementID)
        {
            return deltakerService.getKonkurranseIArrangement(arrangementID);
        }
    }
}
