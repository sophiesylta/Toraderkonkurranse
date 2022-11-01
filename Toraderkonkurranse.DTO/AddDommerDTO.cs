using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.DTO
{
    public class AddDommerDTO
    {
        public AddPersonDTO person { get; set; }
        public int konkurranseID { get; set; }
    }
}
