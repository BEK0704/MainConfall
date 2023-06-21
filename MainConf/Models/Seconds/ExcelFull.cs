using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seconds
{
    public class ExcelFull 
    {
        public string Can_Fio { get; set; }
        public string Can_Pnf { get; set; }
        public string Exp1_Fio { get; set; }
        public string Exp1_Pnf { get; set; }
        public string Exp2_Fio { get; set; }
        public string Exp2_Pnf { get; set; }
        public string Exp3_Fio { get; set; }
        public string Exp3_Pnf { get; set; }
        public string Level { get; set; }
        public string Language { get; set; }
        public string Viloyat { get; set; }
        public int Level_id { get; set; }
        public int Exam_id { get; set; }
        public double Mark1 { get; set; }
        public double Mark2 { get; set; }
        public double Mark3 { get; set; }
        public double LastMark { get; set; }
        public double Audio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Startedtime { get; set; }
        [DataType(DataType.Date)]
        public DateTime Endedtime { get; set; }
        public int Exp1_Word { get; set; }
       
        public int Exp1_Gramma { get; set; }
       
        public int Exp1_Speech { get; set; }
       
        public int Exp1_Communicative { get; set; }
       
        public int Exp1_Pronunciation { get; set; }
        public int Exp2_Word { get; set; }

        public int Exp2_Gramma { get; set; }

        public int Exp2_Speech { get; set; }

        public int Exp2_Communicative { get; set; }

        public int Exp2_Pronunciation { get; set; }
        public int Exp3_Word { get; set; }

        public int Exp3_Gramma { get; set; }

        public int Exp3_Speech { get; set; }

        public int Exp3_Communicative { get; set; }

        public int Exp3_Pronunciation { get; set; }
    }
}
