using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
  public  class RegisterViewModelSection
    {
        [Required]
        [Display(Name = "название участка ")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "idцеха")]
        public int DepartmentId { get; set; }


        [Required]
        [Display(Name = "цех")]
        public Department Department { get; set; }
    }
}
