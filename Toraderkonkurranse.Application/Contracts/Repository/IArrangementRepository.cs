using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.Application.Contracts.Repository
{
    public interface IArrangementRepository
    {
        public void AddArrangement(Arrangement arr);
        public List<Arrangement> GetAlleArrangement();
        public Arrangement GetArrangement(int arrangementID);
        public void AktiverArrangement(int arrID);
        public void AvsluttArrangement(int arrID);
        public Status GetStatus(int arrID);
        public void AddKonkurranse(Konkurranse konk, int arrID);
        public void LeggTilDeltakelse(int arrangementID, Deltakelse deltakelse);
        public List<Score> getScoreListe(int deltakerID, int konkurranseID);
        public string getResultatliste(int konkurranseID);
    }
}
