using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class PhotoE
    {
        [Key]
        public int Id_Photo { get; set; }
        [Required]
        [Display(Name = "Part1")]
        public string Part1 { get; set; }

        [Display(Name = "Part2")]
        public string Part2 { get; set; }

        [Display(Name = "Part3")]
        public string Part3 { get; set; }
        [Display(Name = "Exam_id")]
        public int Exam_id { get; set; }
    }
}
