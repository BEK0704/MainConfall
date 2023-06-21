using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Reading
{
    public class Controlling
    {
        [Key]
        public int Id { get; set; }
        public string Pnfl { get; set; }
        public int Typeerror { get; set; }
        public string Photoer { get; set; }
        public int Status { get; set; }
        public DateTime Errortime { get; set; }
    }
}
