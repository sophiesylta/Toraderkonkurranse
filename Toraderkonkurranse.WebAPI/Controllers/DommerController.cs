using Microsoft.AspNetCore.Mvc;
using Toraderkonkurranse.Application;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;
using Toraderkonkurranse.Infrastructure.Persistence.Context;

namespace Toraderkonkurranse.WebAPI.Controllers
{
    public class DommerController
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

        [HttpPost("oprettDommer")]
        public void opprettDommer(AddPersonDTO personDTO, int konkurranseID)
        {
            dommerService.opprettDommer(personDTO, konkurranseID);
        }
    }
}
