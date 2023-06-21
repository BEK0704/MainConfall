using MainConf.Models.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Listening
{
    public class Showlogs
    {
        public string Fio { get; set; }
        public string Pnfl { get; set; }
        public PhotoE Photo { get; set; }
        public DateTime Dateing { get; set; }
        public List<Controlling> Controllings { get; set; }

    }
}
