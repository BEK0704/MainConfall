using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class ExamRoomr
    {
        [Key]
        public int Id_exam { get; set; }
        public int Can_id { get; set; }
        public int Part1_id { get; set; }
        public int Part2_id { get; set; }
        public int Part3_id { get; set; }
        public int Part4_id { get; set; }
        public int Part5_id { get; set; }
        public int Exp1 { get; set; }
        public int Exp2 { get; set; }
        public int Exp3 { get; set; }
        public int Exp4 { get; set; }
        public int Exp5 { get; set; }
        public double Marks { get; set; }
        public int True_count { get; set; }
        public int False_count { get; set; }
        public int Ended { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Starttime2 { get; set; }
        public DateTime Currenttime { get; set; }
        public DateTime Endtime { get; set; }
        public string Room { get; set; }
    }
}
