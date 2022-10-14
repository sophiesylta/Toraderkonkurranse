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
    public class PersonRepository : IPersonRepository
    {
        private readonly ToraderkonkurranseContext context;
        public PersonRepository(ToraderkonkurranseContext context)
        {
            this.context = context;
        }

        public Person GetPerson(string epost)
        {
            return context.Personer.Where(e => e.epost.Equals(epost)).FirstOrDefault();
        }
        public void LeggTilPerson(Person person)
        {
            context.Personer.Add(person);
            context.SaveChanges();
        }
        public void OppdaterPerson(Person person)
        {
            context.SaveChanges();
        }
    }
}
