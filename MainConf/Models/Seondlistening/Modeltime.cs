using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seondlistening
{
    public class Modeltime
    {
        public DateTime Starttime { get; set; }
        public DateTime Timenow { get; set; }
        public DateTime Currenttime { get; set; }
        public TimeSpan Timers { get; set; }
    }
}
