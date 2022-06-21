using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class RegisterViewModelDepartment
    {

        [Required]
        [Display(Name = "Имя ")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Шифр ")]
        public int IdDepartment { get; set; }

    }
}
