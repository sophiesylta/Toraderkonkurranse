using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;

namespace Toraderkonkurranse.DTO
{
    public class AddDeltakerDTO
    {
        public int arrangementID { get; set; }
        public int konkurranseID { get; set; }
        public string navn { get; set; }
        public List<AddPersonDTO> personer { get; set; }
    }
}
