using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.Application.Contracts.Repository
{
    public interface IDommerRepository
    {
        public List<Konkurranse> getKonkurranseByDommer(int dommerPersonID);
        public List<Deltaker> getDeltakerByKonkurranse(int konkurranseID);
        public void opprettKonkurranseDommer(KonkurranseDommer konkurranseDommer);
        public void setScore(Score score);
    }
}
