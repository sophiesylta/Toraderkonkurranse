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
    public class DeltakerRepository : IDeltakerRepository
    {
        private readonly ToraderkonkurranseContext context;
        public DeltakerRepository(ToraderkonkurranseContext context)
        {
            this.context = context;
        }
        public Deltaker GetDeltaker(int deltakerID)
        {
            return context.Deltakere.Where(e => e.deltakerID == deltakerID).FirstOrDefault();
        }
        public Deltaker GetDeltaker(string deltakerNavn)
        {
            return context.Deltakere.Where(e => e.navn.Equals(deltakerNavn)).Include(e=>e.personer).FirstOrDefault();
        }   
        public void LeggTilPersonIDeltaker(Person person, string deltakerNavn)
        {
            Deltaker deltaker = context.Deltakere.Where(e => e.navn.Equals(deltakerNavn)).FirstOrDefault();
            deltaker.personer.Add(person);

            context.SaveChanges();
        }
        public List<Deltakelse> GetDeltakelseIKonkurranse(int konkurranseID)
        {
            return context.Deltakelse.Where(e => e.konkurranseID == konkurranseID).ToList();
        }
        public Boolean finnesDeltakelseIKonkurranse(int konkurranseID, int deltakerID, int personID)
        {
            return context.Deltakelse.Any(e=>e.konkurranseID == konkurranseID&& e.deltakerID== deltakerID && e.personID == personID);
        }
        public List<Deltakelse> GetDeltakelseIArrangement(int arrangementID) 
        {
            return context.Arrangement.Where(e => e.arrangementID == arrangementID).SelectMany(e => e.deltakelseliste).ToList();
        }
        public void OpprettDeltaker(Deltaker deltaker)
        {
            context.Deltakere.Add(deltaker);
            context.SaveChanges();
        }
        public void OpprettDeltakelse(Deltakelse deltakelse)
        {
            context.Deltakelse.Add(deltakelse);
            context.SaveChanges();
        }
        
    }
}
