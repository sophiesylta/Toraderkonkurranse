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
        public Person GetPerson(string epost);
        public void LeggTilPerson(Person person);
        public void LeggTilPersonIDeltaker(Person person, string deltakerNavn);
        public List<Deltakelse> GetDeltakelseIKonkurranse(int konkurranseID);
        public List<Deltakelse> GetDeltakelseIArrangement(int arrangementID);
        public void OpprettDeltaker(Deltaker deltaker);
        public void OpprettDeltakelse(Deltakelse deltakelse);
    }
}
