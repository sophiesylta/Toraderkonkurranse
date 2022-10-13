using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Application.Contracts.Repository;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.Application
{
    public class DommerService : IDommerService
    {
        private readonly IDommerRepository dommerRepository;
        public DommerService(IDommerRepository dommerRepository)
        {
            this.dommerRepository = dommerRepository;
        }

        public List<Konkurranse> getKonkurranseByDommer(int dommerPersonID)
        {
            return dommerRepository.getKonkurranseByDommer(dommerPersonID);
        }
        public List<Deltaker> getDeltakerByKonkurranse(int konkurranseID)
        {
            return dommerRepository.getDeltakerByKonkurranse(konkurranseID);
        }

    }
}
