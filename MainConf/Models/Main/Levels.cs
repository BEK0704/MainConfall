using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Levels
    {
        [Key]
        public int Id_level { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Names_level { get; set; }
        [Required]
        [Display(Name = "Имя")]
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
