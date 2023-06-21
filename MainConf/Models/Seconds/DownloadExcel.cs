using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seconds
{
    public class DownloadExcel
    {
        [DataType(DataType.Date)]
        public DateTime In_date1 { get; set; }
        [DataType(DataType.Date)]
        public DateTime In_date2 { get; set; }
        public int Location { get; set; }
        public int Level { get; set; }
    }
}
