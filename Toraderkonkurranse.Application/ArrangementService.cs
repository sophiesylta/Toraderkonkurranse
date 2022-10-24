using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Application.Contracts.Repository;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.Application
{
    
    public class ArrangementService : IArrangementService
    {
        private readonly IKonkurranseService konkurranseService;
        private readonly IArrangementRepository arrangementRepository;
        private readonly IDeltakerRepository deltakerRepository;
        private readonly IMapper mapper;

        public ArrangementService(IKonkurranseService konkurranseService, IArrangementRepository arrangementRepository, IDeltakerRepository deltakerRepository, IMapper mapper)
        {
            this.konkurranseService = konkurranseService;
            this.arrangementRepository = arrangementRepository;
            this.deltakerRepository = deltakerRepository;
            this.mapper = mapper;
        }

        public void aktiverArrangement(int arrID)
        {
            arrangementRepository.AktiverArrangement(arrID);
        }

        public void avsluttArrangement(int arrID)
        {
            Arrangement arr = arrangementRepository.GetArrangement(arrID);
            //List<resultat> resultatliste = new();

            foreach (var konk in arr.konkurranseliste)
            {
                List<resultat> resultatliste = new();
                List<int> deltakelse = deltakerRepository.GetDeltakelseIKonkurranse(konk.konkurranseID).Select(e=>e.deltakerID).Distinct().ToList();
                foreach (var deltakerID in deltakelse)
                {
                    resultatliste.Add(samletScore(deltakerID, konk.konkurranseID));
                }
                resultatliste.OrderBy(e=>e.score);
                //resultatlisten blir lagt til i alle konkurranser i arrangementet, selvom deltaker ikke er påmeldt
                konk.resultatliste = JsonSerializer.Serialize(resultatliste); 
            }
            arrangementRepository.AvsluttArrangement(arrID);
        }

        public List<GetArrangementDTO> getAlleArrangement()
        {
            List<Arrangement> arrList = arrangementRepository.GetAlleArrangement();

            return arrList.Select(a => mapper.Map<GetArrangementDTO>(a)).ToList();

        }
        public Arrangement getArrangement(int arrangementID)
        {
            return arrangementRepository.GetArrangement(arrangementID);
        }

        public void opprettArrangement(AddArrangementDTO arrDTO)
        {
            Arrangement arr = new Arrangement()
            {
                arrangor = arrDTO.arrangor,
                lokasjon = arrDTO.lokasjon,
                navn = arrDTO.navn,
                startDato = arrDTO.startDato,
                sluttDato = arrDTO.sluttDato,

                status = Status.planlagt,
            };

            arrangementRepository.AddArrangement(arr);
        }
        public void leggTilDeltakelse(int arrangementID, Deltakelse deltakelse)
        {
            arrangementRepository.LeggTilDeltakelse(arrangementID, deltakelse);
        }
        public void opprettKonkurranse(AddKonkurranseDTO konkDTO)
        {
            Konkurranse konk = new Konkurranse()
            {
                navn = konkDTO.navn,
                maxAntallDeltakere = konkDTO.maxAntallDeltakere,
            };
            int arrID = konkDTO.arrangementID;
            arrangementRepository.AddKonkurranse(konk, arrID);
        }

        public Status getStatus(int arrID)
        {
            return arrangementRepository.GetStatus(arrID);
        }

        public resultat samletScore(int deltakerID, int konkurranseID) 
        {
            int samletScoreVerdi = 0;
            List<Score> scoreList = arrangementRepository.getScoreListe(deltakerID, konkurranseID);
            foreach (var score in scoreList)
            {
                samletScoreVerdi += score.getSamletScore();
            }
            var navn = deltakerRepository.GetDeltaker(deltakerID).navn;
            return new resultat() { navn = navn, score = samletScoreVerdi};

        }

        public string getResultatliste(int konkurranseID)
        {
            return arrangementRepository.getResultatliste(konkurranseID);
        }
    }
}
