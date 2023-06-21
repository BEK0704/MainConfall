using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Logimages
    {
        public string Fio { get; set; }
        public string Pnfl { get; set; }
        public PhotoR Photo { get; set; }
        public DateTime Dateing { get; set; }
        public List<Controlling> Controllings { get; set; }
    }
}
