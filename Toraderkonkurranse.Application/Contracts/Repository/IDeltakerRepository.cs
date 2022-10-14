using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.Application.Contracts.Repository
{
    public interface IDeltakerRepository
    {
        public Deltaker GetDeltaker(int deltakerID);
        public Deltaker GetDeltaker(string deltakerNavn);
        public void LeggTilPersonIDeltaker(Person person, string deltakerNavn);
        public List<Deltakelse> GetDeltakelseIKonkurranse(int konkurranseID);
        public Boolean finnesDeltakelseIKonkurranse(int konkurranseID, int deltakerID, int personID);
        public List<Deltakelse> GetDeltakelseIArrangement(int arrangementID);
        public void OpprettDeltaker(Deltaker deltaker);
        public void OpprettDeltakelse(Deltakelse deltakelse);
    }
}
