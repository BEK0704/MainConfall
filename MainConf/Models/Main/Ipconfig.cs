using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Ipconfig
     
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Ipaddress { get; set; }
        public string Macaddress { get; set; }
        [Required]
        public string Names { get; set; }
        [Required]
        public int Regions { get; set; }
        public int Actived { get; set; }
    }
}
