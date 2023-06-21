using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Speaking
{
    public class GetElements
    {
        public bool Lets { get; set; }
        public string Audiolink { get; set; }
        public string Variantquestion { get; set; }
        public string Question { get; set; }
        public string Imglink { get; set; }
        public int Waitingtime { get; set; }
        public int Recordingtime { get; set; }
        public int Numbers { get; set; }
        public int Part { get; set; }
        public int Question_id{ get; set; }
        
    }
}
