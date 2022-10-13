using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.DTO
{
    public class AddArrangementDTO
    {
        public string arrangor { get; set; }
        public string navn { get; set; }
        public string lokasjon { get; set; }
        public DateTime startDato { get; set; }
        public DateTime sluttDato { get; set; }
    }
}
