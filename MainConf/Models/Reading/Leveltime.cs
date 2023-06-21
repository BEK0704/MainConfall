using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Leveltime
    {
        public int Id { get; set; }
        public int Level_id { get; set; }
        public TimeSpan Timers { get; set; }
      
    }
}
