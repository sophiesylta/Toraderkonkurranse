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

        public DeltakerService(IArrangementService arrangementService, IDeltakerRepository deltakerRepository)
        {
            this.arrangementService = arrangementService;
            this.deltakerRepository = deltakerRepository;
        }
        public Boolean meldPaaDeltaker(int arrangementID, int konkurranseID, AddDeltakerDTO nyDeltakerDTO)
        {
            Deltaker deltaker = deltakerRepository.GetDeltaker(nyDeltakerDTO.navn);
            List<Person> personer = DtoTilPerson(nyDeltakerDTO.personer);

            //sjekk om deltaker eksisterer fra før, hvis ikke, opprett ny
            if (deltaker == null)
            {
                deltaker = new Deltaker();
                deltaker.personer = personer;
                deltaker.navn = nyDeltakerDTO.navn;

                opprettDeltaker(deltaker);
            }
            
            // kan ikke opprette deltaker i et arrangement som er aktivt, avsluttet eller avlyst
            if (arrangementService.getStatus(arrangementID) != Status.planlagt)
            {
                return false;
            }

            foreach (var person in personer)
            {
                //Oppdaterer personer i deltaker
                leggTilPersonIDeltaker(person, deltaker);
                //Oppretter kun deltakelse på personer gitt i dette arrangementet/konkurransen
                opprettDeltakelse(arrangementID, konkurranseID, deltaker.deltakerID, person);
            }
           
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
        public void leggTilPersonIDeltaker(Person person, Deltaker deltaker) 
        {
            if (deltaker.personer.Contains(person))
            {
                return;
            }
            deltakerRepository.LeggTilPersonIDeltaker(person, deltaker.navn);
        }

        private Person leggTilPerson(Person person)
        {
            Person p = deltakerRepository.GetPerson(person.epost);
            if (p == null)
            {
                deltakerRepository.LeggTilPerson(person);
                return person;
            }
            return p;
        }

        // Oppretter deltakelse på hver person i en deltaker
        public void opprettDeltakelse(int arrangementID, int konkurranseID, int deltakerID, Person person) 
        {
            List<Deltakelse> deltakelseList = deltakerRepository.GetDeltakelseIArrangement(arrangementID);

            Deltakelse deltakelse = new Deltakelse()
            {
                deltakerID = deltakerID,
                personID = person.personID,
                konkurranseID = konkurranseID,
            };

            if (deltakelseList.Contains(deltakelse))
            {
                return;
            }
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