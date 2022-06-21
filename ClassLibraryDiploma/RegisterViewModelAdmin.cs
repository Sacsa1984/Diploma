using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class RegisterViewModelAdmin
    {
        [Required]
        [Display(Name = "Password")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Имя ")]
        public string NameHuman { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string Otchestvo { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Табельный номер")]
        public string TabNum { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }
    }
}
