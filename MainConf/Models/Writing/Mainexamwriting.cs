using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Writing
{
    public class Mainexamwriting
    {
        public string Fio { get; set; }
        public string Pnfl { get; set; }
        public string Part1 { get; set; }
        public string Part2 { get; set; }

        public List<Varintwriting> Variantlar { get; set; }
       
        
    }
}
