using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Speaking
{
    public class QuestionsS
    {
        [Key]
        public int Id { get; set; }
        public string Name_q { get; set; }
        public int Var_id { get; set; }
        public int Type_t { get; set; }
        public double Timeing { get; set; }
        public int Waiting_t { get; set; }
        public double Waiting { get; set; }
        public string Audiolink { get; set; }
        public string Photo12 { get; set; }
    }
}
