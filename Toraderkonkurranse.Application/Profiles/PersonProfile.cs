using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.Application.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            //CreateMap<MAPFRA,MAPTIL>();
            CreateMap<AddPersonDTO, Person>();
        }
    }
}
