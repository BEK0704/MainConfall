using MainConf.Models.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.SecondReading
{
    public class Mainexam
    {
        public string Fio { get; set; }
        public string Pnfl { get; set; }
        public List<Variant> Variantlar { get; set; }
        public List<Answers_absd> Javoblar { get; set; }
        public List<Question> Question { get; set; }
    }
}
