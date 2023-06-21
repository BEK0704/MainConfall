using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Notchicked
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Exp1 { get; set; }
        public string Exp2 { get; set; }
        public string Exp3 { get; set; }
        public int Mark1 { get; set; }
        public int Mark2 { get; set; }
        public int Mark3 { get; set; }
        public int Question_id { get; set; }
        public DateTime Insttime { get; set; }
        public string Num_q { get; set; }
    }
}
