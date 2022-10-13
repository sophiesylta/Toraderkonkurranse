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
    public class ArrangementProfile : Profile
    {
        public ArrangementProfile()
        {
            //CreateMap<MAPFRA,MAPTIL>();
            CreateMap<Arrangement, GetArrangementDTO>();
            CreateMap<AddArrangementDTO, Arrangement>();
        }
    }
}
