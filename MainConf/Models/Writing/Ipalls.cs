using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Writing
{
    public class Ipalls
    {
        [Key]
        public int Id { get; set; }
        public string Ipaddress { get; set; }
        public string Macaddress { get; set; }
        public string Names { get; set; }
        public string Regions { get; set; }
        public int Regions_id { get; set; }
        public int Actived { get; set; }
    }
}
