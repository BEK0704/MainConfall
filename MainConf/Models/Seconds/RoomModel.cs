using MainConf.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seconds
{
    public class RoomModel
    {
        public int Id { get; set; }
        public int CanId { get; set; }
        public string Ins_name { get; set; }
        public string Part1 { get; set; }
        public string Part2 { get; set; }
        public string Part3 { get; set; }
        public string CanFirst_name { get; set; }

        public string Exp_pnfl { get; set; }
        public string CanLast_name { get; set; }

        public string CanS_name { get; set; }

        public string CanPnfl { get; set; }

        public string CanPhones { get; set; }

        public string CanPhotos { get; set; }

        public string CanLanguage { get; set; }

        public string CanLevel { get; set; }
        public string First_name { get; set; }

        public string Last_name { get; set; }

        public string S_name { get; set; }
        public int Ended { get; set; }
        public MarkingS Mark6{get; set;}
        public MarkingS Mark5 { get; set; }
        public MarkingS Mark4 { get; set; }
        public MarkingS Mark3 { get; set; }
        public MarkingS Mark2 { get; set; }
        public MarkingS Mark1 { get; set; }
        public MarkingS Mark0 { get; set; }
        public string Entertime { get; set; }
        public string EnterPhoto { get; set; }
        public string Entertol { get; set; }
    }
}
