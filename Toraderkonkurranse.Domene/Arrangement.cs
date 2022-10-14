using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.Domene
{
    public enum Status { planlagt, aktiv, avsluttet, avlyst}
    public class Arrangement
    {
        public Arrangement()
        {
            konkurranser = new List<Konkurranse>();
            deltakelseliste = new List<Deltakelse>();
        }
        public int arrangementID { get; set; }
        public string arrangor { get; set; }
        public string navn { get; set; }
        public string lokasjon { get; set; }
        public DateTime startDato { get; set; }
        public DateTime sluttDato { get; set; }
        private List<Konkurranse> konkurranser { get;  set; }
        public List<Konkurranse> konkurranseliste => konkurranser;
        private List<Deltakelse> deltakelse { get; set; }
        public List<Deltakelse> deltakelseliste { get; set; }
        public Status status { get; set; }

    }
}
