using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Speaking
{
    public class Tematika
    {[Key]
        public int Id { get; set; }
        public string Namet { get; set; }
        public int Lang_id { get; set; }
        public int Actived { get; set; }

    }
}
