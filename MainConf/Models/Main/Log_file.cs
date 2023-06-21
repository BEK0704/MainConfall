using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Main
{
    public class Log_file
    {
        [Key]
        public int Id_logf { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string User_name { get; set; }
        public string Ip_address { get; set; }
        public string Mac_address { get; set; }
        public string Actions { get; set; }
        public DateTime Created_time { get; set; }
    }
}
