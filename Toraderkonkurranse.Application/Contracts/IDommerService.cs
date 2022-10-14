using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.Application.Contracts
{
    public interface IDommerService
    {
        public List<Konkurranse> getKonkurranseByDommer(int dommerPersonID);
        public List<Deltaker> getDeltakerByKonkurranse(int konkurranseID);
        public void opprettDommer(AddPersonDTO personDTO, int konkurranseID);
    }
}
