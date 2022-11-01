using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;
using Toraderkonkurranse.Infrastructure.Persistence.Context;

namespace Toraderkonkurranse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArrangementController : ControllerBase
    {
        private readonly IArrangementService arrangementService;

        public ArrangementController(IArrangementService arrangementService)
        {
            this.arrangementService = arrangementService;
        }

        [HttpPost("OpprettArrangement")]
        public void opprettArrangement(AddArrangementDTO arrDTO)
        {
            arrangementService.opprettArrangement(arrDTO);
        }

       

        [HttpGet("aktiverArrangement")]
        public void aktiverArrangement(int arrangmentID)
        {
            arrangementService.aktiverArrangement(arrangmentID);
        }

        [HttpGet("avsluttArrangement")]
        public void avsluttArrangement(int arrangementID)
        {
            arrangementService.avsluttArrangement(arrangementID);
        }

        [HttpPost("OpprettKonkurranse")]
        public void opprettKonkurranse(AddKonkurranseDTO konkDTO)
        {
            arrangementService.opprettKonkurranse(konkDTO);
        }

        [HttpGet("getResultatliste")]
        public string getResultatliste(int konkurranseID)
        {
            string resultat = arrangementService.getResultatliste(konkurranseID);
            return resultat; //arrangementService.getResultatliste(konkurranseID);
        }
    }
}


