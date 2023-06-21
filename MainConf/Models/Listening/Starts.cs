using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Listening
{
    public class Starts
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Reading { get; set; }
        public string Seted_by{ get; set; }
        public int Listening { get; set; }
        public int Speaking { get; set; }
        public int Writing { get; set; }

    }
}
