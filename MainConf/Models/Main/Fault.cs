using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Fault
    {
        [Key]
        public int Id_fault { get; set; }
        public int Can_id { get; set; }

        public int Exp_id { get; set; }
        public int Exm_id { get; set; }
        public string Room { get; set; }
        public string Caouse { get; set; }

    }
}
