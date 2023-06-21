using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Marks
    {
        [Key]
        public int Id_mark { get; set; }
        [Required]
        [Display(Name = "Word")]
        public int Word { get; set; }
        [Required]
        [Display(Name = "Gramma")]
        public int Gramma { get; set; }
        [Required]
        [Display(Name = "Speech")]
        public int Speech { get; set; }
        [Required]
        [Display(Name = "Communicative")]
        public int Communicative { get; set; }
        [Required]
        [Display(Name = "Pronunciation")]
        public int Pronunciation { get; set; }
        [Required]
        [Display(Name = "Exam_id")]
        public int Exam_id { get; set; }
        [Required]
        [Display(Name = "Typem")]
        public int Typem { get; set; }
        public string Comments { get; set; }
        public DateTime Markedtime { get; set; }
    }
}
