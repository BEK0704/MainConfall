using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Name_q { get; set; }
        public int Var_id { get; set; }
        public int Type_q { get; set; }
        public string Ansv { get; set; }
    }
}
