using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Writing
{
    public class Examwriting
    {
        [Key]
        public int Id { get; set; }
        
        public int Cand_id { get; set; }

        public int Export1_id { get; set; }

        public int Export2_id { get; set; }

        public int Export3_id { get; set; }
        
        public DateTime Started_time { get; set; }

        public DateTime Ended_time { get; set; }
        public int Ended { get; set; }
        
        public int Part1_id { get; set; }
        public int Part2_id { get; set; }
        public int Exp1 { get; set; }
        public int Exp2 { get; set; }
        public double Mark1 { get; set; }
        public double Mark2 { get; set; }
        public double Mark3 { get; set; }
        public double LastMark { get; set; }
        public DateTime Starttime2 { get; set; }
        public DateTime Starttime3 { get; set; }
       
    }
}
