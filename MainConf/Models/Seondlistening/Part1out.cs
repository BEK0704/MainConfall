using MainConf.Models.Listening;
using MainConf.Models.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seondlistening
{
    public class Part1out
    {
       public int Voluem { get; set; }
        public string Fio { get; set; }
        public string Pnf { get; set; }
        public string Startaudio { get; set; }
        public string Intradacauido { get; set; }
        public string Mainaudio { get; set; }
        public string Endaudio { get; set; }
        public Listvariant Variantlar { get; set; }
        public List<Answers_absd> Javoblar { get; set; }
        public List<Question> Question { get; set; }
    }
}
