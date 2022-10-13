using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.Domene
{
    public class Person
    {
        public int personID { get; set; }
        public string fornavn { get; set; } = "";
        public string etternavn { get; set; } = "";
        public string epost { get; set; } = "";
        public Boolean erDommer { get; set; } = false;
    }
}
