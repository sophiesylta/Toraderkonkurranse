using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.Application.Contracts
{
    public interface IDeltakerService
    {
        public Boolean meldPaaDeltaker(int arrangementID, int konkurranseID, AddDeltakerDTO nyDeltakerDTO);
        public void leggTilNyePersonerIDeltaker(Person person, Deltaker deltaker);
        public void opprettDeltakelse(int arrangementID, int konkurranseID, int deltakerID, int personID);
        public List<GetArrangementDTO> getAlleArrangement();
        public List<Konkurranse> getKonkurranseIArrangement(int arrangementID);
    }
}
