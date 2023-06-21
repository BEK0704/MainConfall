using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Writing
{
    public class Ansvercand
    {
        [Key]
        public int Id { get; set; }
        public string Part1full { get; set; }
        public string Part2full { get; set; }
        public string Part1txt { get; set; }
        public string Part2txt { get; set; }
        public int Count1 { get; set; }
        public int Count2 { get; set; }
        public int Exam_id { get; set; }
    }
}
