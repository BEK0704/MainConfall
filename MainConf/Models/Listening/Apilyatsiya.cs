using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Listening
{
    public class Apilyatsiya
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Part { get; set; }
        [Required]
        public int Var_id { get; set; }
        [Required]
        public int Question_id { get; set; }
        [Required]
        public string Mains { get; set; }
        public string Created_by { get; set;}
        public DateTime Date_time { get; set; }
    }
}
