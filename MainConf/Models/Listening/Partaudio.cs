using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Listening
{
    public class Partaudio
    {
        [Key]
        public int Id { get; set; }
        public string Audio_int { get; set; }
        public string Audio_end { get; set; }
        public int Lang_id { get; set; }
        public int Part_num { get; set; }
        public int Actived { get; set; }
        public string Created_by { get; set; }
        public DateTime Created_time { get; set; }
        public string Updated_by { get; set; }
        public DateTime Updated_time { get; set; }
    }
}
