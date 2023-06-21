using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "PNFL")]
        public string Pnfl { get; set; }


        [Required]
        [Display(Name = "Должность")]
        public string Position { get; set; }


 
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        
        
        
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
