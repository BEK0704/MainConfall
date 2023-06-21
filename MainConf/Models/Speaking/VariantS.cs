using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Speaking
{
    public class VariantS
    {
        [Key]
        public int Id { get; set; }
        public string Name_var { get; set; }
        public int Lan_id { get; set; }
        public string Q_text { get; set; }
        public int Type_p { get; set; }
        public int Tema { get; set; }
        public int Actived { get; set; }
        public string Created_by { get; set; }
        public DateTime Created_time { get; set; }
        public string Updated_by { get; set; }
        public DateTime Updated_time { get; set; }
       

    }
}
