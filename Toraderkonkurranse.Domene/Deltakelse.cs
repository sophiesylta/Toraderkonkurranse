using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.Domene
{
    public enum DeltakelseStatus {påmeldt, aktiv, avsluttet, avbrutt }
    public class Deltakelse 
    {
        public Deltakelse(int deltakerID, int konkurranseID, int personID, DeltakelseStatus status = DeltakelseStatus.påmeldt)
        {
            this.deltakerID = deltakerID;
            this.konkurranseID = konkurranseID;
            this.personID = personID;
            this.status = status;
        }

        public int deltakelseID { get; private set; }
        public int deltakerID { get; init; }
        public int konkurranseID { get; init; }
        public int personID { get; init; }
        public DeltakelseStatus status { get; set; }
        public Arrangement arrangement { get; init; }

    }
}
