using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Listening
{
    public class Erroparts
    {
       public string Pnfl { get; set; } 
        public string Part1 { get; set; }
        public string Part2 { get; set; }
        public string Part3 { get; set; }
        public string Part4 { get; set; }
        public string Part5 { get; set; }
        public string Part6 { get; set; }
        [Required]
        public string Reason { get; set; } 
        

    }
}
