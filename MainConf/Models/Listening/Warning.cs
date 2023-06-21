using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Listening
{
    public class Warning
    {
        [Key]
        public int Id { get; set; }
        public string Wartxt { get; set; }
        public int Exam_id { get; set; }
        public int Part { get; set; }
        public string Created_by { get; set; }
        public DateTime Created_time { get; set; }
        public int Actived { get; set; }
    }
}
