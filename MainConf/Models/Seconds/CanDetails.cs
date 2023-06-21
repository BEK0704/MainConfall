using MainConf.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seconds
{
    public class CanDetails 
    {
        public int Can_id { get; set; }
        public string Photo { get; set; }
        public string Pnfl { get; set; }
        public string Can_fio { get; set; }
        public string Lan { get; set; }
        public string Lev { get; set; }
        public string Region { get; set; }
        public string Starttime { get; set; }
        public string Endtime { get; set; }
        public int Exp1 { get; set; }
        public string Part1 { get; set; }
        public string Part2 { get; set; }
        public string Part3 { get; set; }
        public Marks Mark1{ get; set;}
        public string Exp1_fio { get; set; }
        public double Exp1_mark { get; set; }
        public string Audiolink { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public int Exp2 { get; set; }
        public string Exp2_fio { get; set; }
        public Marks Mark2 { get; set; }
        public double Exp2_mark { get; set; }
        public double LastM { get; set; }
        public int Exp3 { get; set; }
        public string Exp3_fio { get; set; }
        public string Entertime { get; set; }
        public string EnterPhoto { get; set; }
        public string Entertol { get; set; }
        public double Exp3_mark { get; set; }
        public Marks Mark3 { get; set; }

    }
}
