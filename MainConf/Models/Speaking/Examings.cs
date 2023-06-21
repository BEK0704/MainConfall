using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Speaking
{
    public class Examings
    {
        [Key]
        public int Id { get; set; } 
        public int Can_id { get; set; } 
        public int Tem_id { get; set; } 
        public int Part1_id { get; set; } 
        public int Part2_id { get; set; } 
        public int Part3_id { get; set; }
        public int Exp1 { get; set; } 
        public int Exp2 { get; set; } 
        public int Exp3 { get; set; }
        public int Real_q { get; set; }
        public int Next_q { get; set; }
        public int Par_real { get; set; }
        public double Mark1 { get; set; } 
        public double Mark2 { get; set; } 
        public double Mark3 { get; set; } 
        public double Lastmark { get; set; } 
        public DateTime Starttime { get; set; } 
        public DateTime Endtime { get; set; }
        public int End { get; set; }
    }
}
