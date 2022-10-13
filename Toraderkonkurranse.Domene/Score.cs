using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toraderkonkurranse.Domene
{
    public class Score
    {
        public int scoreID { get; set; }
        public int deltakerID { get; set; }
        public int konkurranseDommerID { get; set; }
        public int konkurranseID { get; set; }
        public int taktScore { get; set; }
        public int arrangementScore { get; set; }
        public int formidlingScore { get; set; }
        public int teknikkScore { get; set; }

        public int getSamletScore()
        {
            return taktScore + arrangementScore + formidlingScore + teknikkScore;
        }
    }
}
