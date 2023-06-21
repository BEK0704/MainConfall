using MainConf.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models.Seconds
{
    public class FaceDetailsModel
    {
        public string  Usertype { get; set; }
        public string Photo { get; set; }
        public string Pnfl { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
        public int Soni { get; set; }
        public List<Facelog> Faces { get; set; }
    }
}
