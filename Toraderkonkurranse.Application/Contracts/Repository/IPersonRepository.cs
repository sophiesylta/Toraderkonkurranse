using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.Application.Contracts.Repository
{
    public interface IPersonRepository
    {
        public Person GetPerson(string epost);
        public void LeggTilPerson(Person person);
        public void OppdaterPerson(Person person);
    }
}
