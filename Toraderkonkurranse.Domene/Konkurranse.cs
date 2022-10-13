using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.Domene
{
    public class Konkurranse
    {
  
        public int konkurranseID { get; set; }
        public string navn { get; set; }
        public int maxAntallDeltakere { get; set; }
        public string resultatliste { get; set; } = "";
        public DateTime tidspunkt { get; set; }

    }
}
