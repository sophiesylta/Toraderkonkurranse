using Microsoft.AspNetCore.Mvc;
using Toraderkonkurranse.Application;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;
using Toraderkonkurranse.Infrastructure.Persistence.Context;

namespace Toraderkonkurranse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DommerController : ControllerBase
    {
        private readonly IDommerService dommerService;
        public DommerController(IDommerService dommerService)
        {
            this.dommerService = dommerService;
        }

        [HttpGet("getKonkurranseByDommer")]
        public List<Konkurranse> getKonkurranseByDommer(int dommerPersonID)
        {
            return dommerService.getKonkurranseByDommer(dommerPersonID);

        }

        [HttpGet("getDeltakerByKonkurranse")]
        public List<Deltaker> getDeltakerByKonkurranse(int konkurranseID)
        {
            return dommerService.getDeltakerByKonkurranse(konkurranseID);
        }

        [HttpPost("opprettDommer")]
        public void opprettDommer(AddDommerDTO dommerDTO)
        {
            dommerService.opprettDommer(dommerDTO.person, dommerDTO.konkurranseID);
        }
        [HttpPost("setScore")]
        public void setScore(int konkurranseID, int deltakerID, int dommerPersonID, int arrangementScore, int formidlingScore, int taktScore, int teknikkScore)
        {
            dommerService.setScore(konkurranseID, deltakerID, dommerPersonID, arrangementScore, formidlingScore, taktScore, teknikkScore);
        }
    }
}
