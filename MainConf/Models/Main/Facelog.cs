using System;

using System.ComponentModel.DataAnnotations;


namespace MainConf.Models.Main
{
    public class Facelog
    {
        [Key]
        public int Id_facelog { get; set; }
        public string Pnfl { get; set; }
        public string Ip_address { get; set; }
        public string Mac { get; set; }
        public string Images { get; set; }
        public DateTime Created_time { get; set; }
        public int User_type { get; set; }
        public int Entered { get; set; }
        public string Tol { get; set; }
    }
}
