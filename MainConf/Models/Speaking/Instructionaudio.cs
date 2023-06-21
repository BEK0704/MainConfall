using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Speaking
{
    public class Instructionaudio
    {
        [Key]
        public int Id { get; set; }
        public int Lan_id { get; set; }
        public string Linking { get; set; }
        public int Type_p { get; set; }
    }
}
