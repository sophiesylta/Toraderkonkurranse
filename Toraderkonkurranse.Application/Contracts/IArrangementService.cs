using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toraderkonkurranse.Domene;
using Toraderkonkurranse.DTO;

namespace Toraderkonkurranse.Application.Contracts
{
    public class resultat
    {
        public int score { get; set; }
        public string navn { get; set; }
    }
    public interface IArrangementService
    {
        public void opprettArrangement(AddArrangementDTO arrDTO);
        public List<GetArrangementDTO> getAlleArrangement();
        public Arrangement getArrangement(int arrangementID);
        public void aktiverArrangement(int arrID);
        public void avsluttArrangement(int arrID);
        public Status getStatus(int arrID);
        public void opprettKonkurranse(AddKonkurranseDTO konkDTO);
        public void leggTilDeltakelse(int arrangementID, Deltakelse deltakelse);
        public resultat samletScore(int deltakerID, int konkurranseID);
        public string getResultatliste(int konkurranseID);
    }
}
