﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Part1
    {
        [Key]
        public int Id_part1 { get; set; }
        [Required]
        [Display(Name = "Question")]
        public string Question { get; set; }
        [Required]
        [Display(Name = "Time")]
        public int Timeq { get; set; }
        [Required]
        [Display(Name = "Level")]
        public int Level_id { get; set; }
        [Display(Name = "Actived")]
        public int Actived { get; set; }

        [Display(Name = "Created_by ")]
        public string Created_by { get; set; }

        [Display(Name = "Created_time ")]
        public DateTime Created_time { get; set; }

        [Display(Name = "Updated_by")]
        public string Updated_by { get; set; }

        [Display(Name = "Updated_time ")]
        public DateTime Updated_time { get; set; }
        [Required]
        [Display(Name = "Title ")]
        public string Title { get; set; }

    }
}
