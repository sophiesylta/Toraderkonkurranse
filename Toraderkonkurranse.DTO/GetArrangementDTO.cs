using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.DTO
{
    public class GetArrangementDTO
    {
        public int arrangementID { get; set; }
        public string arrangor { get; set; }
        public string navn { get; set; }
        public string lokasjon { get; set; }
        public DateTime startDato { get; set; }
        public DateTime sluttDato { get; set; }
        private List<Konkurranse> konkurranser { get; set; }
        public string status { get; set; }
    }
}
