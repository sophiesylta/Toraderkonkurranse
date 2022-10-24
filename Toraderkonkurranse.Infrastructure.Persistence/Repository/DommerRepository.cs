using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Application.Contracts.Repository;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.Infrastructure.Persistence.Context;

namespace Toraderkonkurranse.Infrastructure.Persistence.Repository
{
    public class DommerRepository : IDommerRepository
    {
        private readonly ToraderkonkurranseContext context;
        public DommerRepository(ToraderkonkurranseContext context)
        {
            this.context = context;
        }
        public List<Konkurranse> getKonkurranseByDommer(int dommerPersonID)
        {
            var konkurranserID = context.KonkurranseDommer.Where(e => e.personID == dommerPersonID).Select(e => e.konkurranseID).ToList();
            List<Konkurranse> konkurranser = context.Konkurranser.Where(e => konkurranserID.Contains(e.konkurranseID)).ToList();
            return konkurranser;

        }
        public List<Deltaker> getDeltakerByKonkurranse(int konkurranseID)
        {
            List<int> IDdeltakere = context.Deltakelse.Where(e => e.konkurranseID == konkurranseID).Select(e => e.deltakerID).Distinct().ToList();
            List<Deltaker> deltakere = context.Deltakere.Where(e => IDdeltakere.Contains(e.deltakerID)).Include(e => e.personer).ToList();

            return deltakere;
        }
        public void opprettKonkurranseDommer(KonkurranseDommer konkurranseDommer)
        {
            context.KonkurranseDommer.Add(konkurranseDommer);
            context.SaveChanges();
        }
        public void setScore(Score score)
        {
            context.Score.Add(score);
            context.SaveChanges();
        }
    }
}
