using Toraderkonkurranse.Domene;
using Toraderkonkurranse.Infrastructure.Persistence.Context;

namespace Toraderkonkurranse.Infrastructure.Persistence
{
    public class PersonRepository
    {
        private readonly ToraderkonkurranseContext toraderkonkurranseContext;

        public PersonRepository(ToraderkonkurranseContext toraderkonkurranseContext)
        {
            this.toraderkonkurranseContext = toraderkonkurranseContext;
        }
    }
}