using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class LoginModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя ")]
        
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан табельный номер")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Display(Name = "Запомнить?")]
        //public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
