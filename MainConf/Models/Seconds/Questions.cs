using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seconds
{
    public class Questions
    {
        public int Id_part { get; set; }
        
        public string Question { get; set; }
        public int Timeq { get; set; }
        public string Level { get; set; }
        public int Level_id { get; set; }
        public int Actived { get; set; }
        public string Title { get; set; }

    }
}
