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
    public class ArrangementRepository : IArrangementRepository
    {
        private readonly ToraderkonkurranseContext context;
        public ArrangementRepository(ToraderkonkurranseContext context)
        {
            this.context = context;
        }
        public void AddArrangement(Arrangement arr)
        {
            context.Arrangement.Add(arr);
            context.SaveChanges();
        }

        public List<Arrangement> GetAlleArrangement()
        {
            return context.Arrangement.Include(e=>e.konkurranseliste).ToList();
        }

        public Arrangement GetArrangement(int arrangementID)
        {
            return context.Arrangement.Where(e => e.arrangementID == arrangementID).Include(e=>e.konkurranseliste).FirstOrDefault();
        }

        public void AktiverArrangement(int arrangmentID)
        {
            var arr = context.Arrangement.Where(e => e.arrangementID == arrangmentID).Include(e => e.konkurranseliste).FirstOrDefault();
            arr.status = Status.aktiv;

            foreach (var konkurranse in arr.konkurranseliste)
            {
                var deltakelseListe = context.Deltakelse.Where(e => e.konkurranseID == konkurranse.konkurranseID).ToList();
                foreach (var deltakelse in deltakelseListe)
                {
                    deltakelse.status = DeltakelseStatus.aktiv;
                }
            }
            context.SaveChanges();
        }

        public void AvsluttArrangement(int arrID)
        {
            var arr = context.Arrangement.Where(e => e.arrangementID == arrID).Include(e => e.konkurranseliste).FirstOrDefault();
            arr.status = Status.avsluttet;

            foreach (var konkurranse in arr.konkurranseliste)
            {
                var deltakelseListe = context.Deltakelse.Where(e => e.konkurranseID == konkurranse.konkurranseID).ToList();
                foreach (var deltakelse in deltakelseListe)
                {
                    deltakelse.status = DeltakelseStatus.avsluttet;
                }
            }
            context.SaveChanges();
        }

        public Status GetStatus(int arrID)
        {
            return context.Arrangement.Where(e => e.arrangementID == arrID).Select(e => e.status).FirstOrDefault();
        }

        public void AddKonkurranse(Konkurranse konk, int arrID)
        {
            Arrangement arr = context.Arrangement.Where(e=> e.arrangementID == arrID).FirstOrDefault();
            arr.konkurranseliste.Add(konk);
            context.Update(arr);
            context.SaveChanges();
        }
        public void LeggTilDeltakelse(int arrangementID, Deltakelse deltakelse)
        {
            Arrangement arr = context.Arrangement.Where(e => e.arrangementID == arrangementID).FirstOrDefault();
            arr.deltakelseliste.Add(deltakelse);
            context.Update(arr);
            context.SaveChanges();
        }
        public List<Score> getScoreListe(int deltakerID, int konkurranseID)
        {
            return context.Score.Where(e => e.deltakerID == deltakerID).Where(e=>e.konkurranseID == konkurranseID).ToList();
        }

        public string getResultatliste(int konkurranseID)
        {
            return context.Konkurranser.Where(e => e.konkurranseID == konkurranseID).Select(e => e.resultatliste).FirstOrDefault();
        }
    }
}
