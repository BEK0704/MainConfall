using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Kickis
    {
        [Key]
        public int Id { get; set; }
        public string Candidate { get; set; }
        public string Reason { get; set; }
        public string Admins { get; set; }
        public DateTime Intime { get; set; }
    }
}
