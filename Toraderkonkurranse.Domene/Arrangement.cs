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

        public Boolean leggTilKonkurranse(Konkurranse konkurranse) 
        {
            //TODO: sjekk at plass til ny konkurranse
            konkurranser.Add(konkurranse);
            return true;
        }
        public Boolean meldPaaDetaker(Deltaker deltaker, int konkurranseID)
        {
            foreach (var person in deltaker.personer)
            {
                Deltakelse deltakelse = new Deltakelse()
                {
                    status = DeltakelseStatus.påmeldt,
                    konkurranseID = konkurranseID,
                    deltakerID = deltaker.deltakerID,
                    personID = person.personID,
                };
                deltakelseliste.Add(deltakelse);
            }
            //TODO: sjekk at det er plass til deltakelse
            //TODO: sjekk at deltakelse ikke finnes fra før

            return true;
        }
    }
}
