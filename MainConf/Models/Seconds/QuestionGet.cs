using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seconds
{
    public class QuestionGet
    {
        public string Question { get; set; }
        public int Usertype { get; set; }
        public int Part { get; set; }
        public int Ended { get; set; }
        public string Alltime { get; set; }
        public string Ntime { get; set; }
        public string Qtime { get; set; }
        public string Level { get; set; }
        public int Expid { get; set; }
    }
}
