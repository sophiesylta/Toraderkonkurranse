using System.Linq;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Application.Contracts.Repository;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.Application
{
    public class DeltakerService : IDeltakerService
    {
        private readonly IArrangementService arrangementService;
        private readonly IDeltakerRepository deltakerRepository;
        private readonly IPersonRepository personRepository;

        public DeltakerService(IArrangementService arrangementService, IDeltakerRepository deltakerRepository, IPersonRepository personRepository)
        {
            this.arrangementService = arrangementService;
            this.deltakerRepository = deltakerRepository;
            this.personRepository = personRepository;
        }
        public Boolean meldPaaDeltaker(int arrangementID, int konkurranseID, AddDeltakerDTO nyDeltakerDTO)
        {
            //TODO sjekk at konkurranse finnes i arrangement

            // kan ikke opprette deltaker i et arrangement som er aktivt, avsluttet eller avlyst
            if (arrangementService.getStatus(arrangementID) != Status.planlagt)
            {
                return false;
            }

            Deltaker deltaker = deltakerRepository.GetDeltaker(nyDeltakerDTO.navn);
            //TODO bruke mapper
            List<Person> personer = DtoTilPerson(nyDeltakerDTO.personer);

            //oppretter personer i listen som ikke finnes
            personer = personer.Select(e=>leggTilPerson(e)).ToList();

            //sjekk om deltaker eksisterer fra før, hvis ikke, opprett ny
            if (deltaker == null)
            {
                deltaker = new Deltaker(nyDeltakerDTO.navn, personer);

                opprettDeltaker(deltaker);
            }
            else 
            {
                //Oppdaterer personer i deltaker
                personer.ForEach(person => leggTilNyePersonerIDeltaker(person, deltaker));
            }
            
            //Oppretter kun deltakelse på personer gitt i dette arrangementet/konkurransen
            personer.ForEach(person => opprettDeltakelse(arrangementID, konkurranseID, deltaker.deltakerID, person.personID));
            
            return true;
        }

        public void opprettDeltaker(Deltaker deltaker)
        {
            foreach (var person in deltaker.personer)
            {
                var p = leggTilPerson(person);
            }

            deltakerRepository.OpprettDeltaker(deltaker);
        }

        public List<Person> DtoTilPerson(List<AddPersonDTO> dtoPersoner)
        {
            List<Person> personer = new List<Person>();
            foreach (var dto in dtoPersoner)
            {
                personer.Add(new Person()
                {
                    fornavn = dto.fornavn,
                    etternavn = dto.etternavn,
                    epost = dto.epost,
                });
            }
            return personer;
        }

        //legger til personer i deltaker sin liste av personer
        public void leggTilNyePersonerIDeltaker(Person person, Deltaker deltaker) 
        {
            if (deltaker.personer.Any(p=> p.epost.Equals(person.epost)))
            {
                return;
            }
            deltakerRepository.LeggTilPersonIDeltaker(person, deltaker.navn);
        }

        private Person leggTilPerson(Person person)
        {
            Person p = personRepository.GetPerson(person.epost);
            if (p == null)
            {
                personRepository.LeggTilPerson(person);
                return person;
            }
            return p;
        }

        // Oppretter deltakelse på hver person i en deltaker
        public void opprettDeltakelse(int arrangementID, int konkurranseID, int deltakerID, int personID) 
        {
            if (deltakerRepository.finnesDeltakelseIKonkurranse(konkurranseID, deltakerID, personID))
            {
                return;
            }
            Deltakelse deltakelse = new Deltakelse(deltakerID, konkurranseID, personID);

            //legge til deltakelsen i arrangementet sin liste av deltakelser
            arrangementService.leggTilDeltakelse(arrangementID, deltakelse);

        }

        public List<GetArrangementDTO> getAlleArrangement()
        {
            return arrangementService.getAlleArrangement();
        }

        public List<Konkurranse> getKonkurranseIArrangement(int arrangementID)
        {
            return arrangementService.getArrangement(arrangementID).konkurranseliste;
        }
    }
}