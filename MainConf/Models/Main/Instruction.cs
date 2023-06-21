using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Instruction
    {


        [Key]
        public int Id_instruc { get; set; }
        [Required]
        [Display(Name = "Part 1")]
        public string Instruct_1 { get; set; }

        [Required]
        [Display(Name = "Part 2")]
        public string Instruct_2 { get; set; }

        [Required]
        [Display(Name = "Part 3")]
        public string Instruct_3 { get; set; }
        [Required]
        [Display(Name = "Language ")]
        public int Lang_id { get; set; }
        [Required]
        [Display(Name = "Part 3")]
        public string Name_i { get; set; }
        
        [Display(Name = "Created_by ")]
        public string Created_by { get; set; }

        [Display(Name = "Created_time ")]
        public DateTime Created_time { get; set; }

        [Display(Name = "Updated_by")]
        public string Updated_by { get; set; }

        [Display(Name = "Updated_time ")]
        public DateTime Updated_time { get; set; }
    }
}
