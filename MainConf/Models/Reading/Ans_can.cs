using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Ans_can
    {
        [Key]
        public int Id { get; set; }
        public string Answar { get; set; }
        public int Exam_id { get; set; }
        public string Parts { get; set; }
        public DateTime Last_time { get; set; }
        public int True_false { get; set; }
        public int Ques_id { get; set; }
        public string Num_q { get; set; }
    }
}
