using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Exams
    {
        [Key]
        public int Id_exam { get; set; }
        [Required]
        [Display(Name = "Cadidate")]
        public int Cand_id { get; set; }

        [Display(Name = "Export1")]
        public int Export1_id { get; set; }

        [Display(Name = "Export2 ")]
        public int Export2_id { get; set; }

        [Display(Name = "Examiner_id ")]
        public int Examiner_id { get; set; }
        [Display(Name = "Room ")]
        public string Room { get; set; }

        [Display(Name = "Started_time")]
        public DateTime Started_time { get; set; }

        [Display(Name = "Ended_time")]
        public DateTime Ended_time { get; set; }
        public int Ended { get; set; }
        public int SecondEnd { get; set; }
        public int Part1_id { get; set; }
        public int Part2_id { get; set; }
        public int Part3_id { get; set; }
        public double Mark1 { get; set; }
        public double Mark2 { get; set; }
        public double Mark3 { get; set; }
        public double LastMark { get; set; }
        public DateTime Starttime2 { get; set; }
        public DateTime Starttime3 { get; set; }
        public int Exclude { get; set; }

    }
}
