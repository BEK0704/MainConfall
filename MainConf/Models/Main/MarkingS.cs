using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class MarkingS
    {

        [Key]
        public int Id_marking { get; set; }
        [Required]
        [Display(Name = "Word")]
        public string Word { get; set; }
        [Required]
        [Display(Name = "Gramma")]
        public string Gramma { get; set; }
        [Required]
        [Display(Name = "Speech")]
        public string Speech { get; set; }
        [Required]
        [Display(Name = "Communicative")]
        public string Communicative { get; set; }
        [Required]
        [Display(Name = "Pronunciation")]
        public string Pronunciation { get; set; }
        [Required]
        [Display(Name = "Level_id")]
        public int Level_id { get; set; }
        [Required]
        [Display(Name = "Local_id")]
        public int Local_id { get; set; }
        [Required]
        [Display(Name = "Mark_id")]
        public int Mark_id { get; set; }
    }
}
