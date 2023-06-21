using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Candidatesr
    {
        [Key]
        public int Id_candidate { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string First_name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Last_name { get; set; }

        [Required]
        [Display(Name = "Отчества")]
        public string S_name { get; set; }
        [Required]
        [Display(Name = "Pnfl")]
        public string Pnfl { get; set; }

        public int Level_id { get; set; }
        [Required]
        [Display(Name = "Group")]
        public int Group_id { get; set; }
        [Required]
        [Display(Name = "Language")]
        public int Lang_id { get; set; }


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
