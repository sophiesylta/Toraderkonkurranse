using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.Domene
{
    public enum DeltakelseStatus {påmeldt, aktiv, avsluttet, avbrutt }
    public class Deltakelse : IEquatable<Deltakelse>
    {
        public int deltakelseID { get; set; }
        public int deltakerID { get; set; }
        public int konkurranseID { get; set; }
        public int personID { get; set; }
        public DeltakelseStatus status { get; set; }

        public bool Equals(Deltakelse obj)
        {
            return this.deltakerID == obj.deltakerID && this.konkurranseID == obj.konkurranseID && this.personID == obj.personID;
        }

    }
}
