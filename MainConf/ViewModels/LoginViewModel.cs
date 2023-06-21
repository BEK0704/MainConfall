using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "PNFL")]
        public string Pnfl { get; set; }
        

        public string ReturnUrl { get; set; }
    }
}
