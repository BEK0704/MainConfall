using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PNFL { get; set; }
        public string Position { get; set; }
        public string Is_active { get; set; }   
    }
}
