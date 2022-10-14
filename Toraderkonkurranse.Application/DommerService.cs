using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Application.Contracts.Repository;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.Application
{
    public class DommerService : IDommerService
    {
        private readonly IDommerRepository dommerRepository;
        private readonly IMapper mapper;
        private readonly IPersonRepository personRepository;

        public DommerService(IDommerRepository dommerRepository, IMapper mapper, IPersonRepository personRepository)
        {
            this.dommerRepository = dommerRepository;
            this.mapper = mapper;
            this.personRepository = personRepository;
        }

        //TODO: sjekke om person er deltaker, sjekke om konkurranseDommer eksisterer
        public void opprettDommer(AddPersonDTO personDTO, int konkurranseID)
        {
            Person personDommer = personRepository.GetPerson(personDTO.epost);
            if (personDommer == null)
            {
                personDommer = mapper.Map<Person>(personDTO);
                personRepository.LeggTilPerson(personDommer);
            }
            personDommer.erDommer = true;

            personRepository.OppdaterPerson(personDommer);
            
            KonkurranseDommer konkurranseDommer = new KonkurranseDommer()
            {
                konkurranseID = konkurranseID,
                personID = personDommer.personID
            };
            dommerRepository.opprettKonkurranseDommer(konkurranseDommer);
        }

        public List<Konkurranse> getKonkurranseByDommer(int dommerPersonID)
        {
            return dommerRepository.getKonkurranseByDommer(dommerPersonID);
        }
        public List<Deltaker> getDeltakerByKonkurranse(int konkurranseID)
        {
            return dommerRepository.getDeltakerByKonkurranse(konkurranseID);
        }

    }
}
