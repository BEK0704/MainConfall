using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Logreading
    {
        [Key]
        public int Id { get; set; }
        public string Admin_id { get; set; }
        public string Cand_id { get; set; }
        public DateTime Startedtime { get; set; }
        public DateTime Endedtime { get; set; }
        public int Ended { get; set; }
        public int Warnings { get; set; }
    }
}
