using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Answers_absd
    {
        [Key]
        public int Id_ans { get; set; }
        public string Answers { get; set; }
        public int Question_id { get; set; }
        public int Trueorfalse { get; set; }
        public string Created_by { get; set; }
        public string Updated_by { get; set; }
        public DateTime Created_time { get; set; }
        public DateTime Updated_time { get; set; }

    }
}
