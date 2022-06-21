using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class RegisterViewModelEquipment
    {
        [Required]
        [Display(Name = "Имя ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "инвертарный номер")]
        public string INN { get; set; }

        [Required]
        [Display(Name = "краткие характеристики ")]
        public string Characteristics { get; set; }

        [Required]
        [Display(Name = "id участка ")]
        public int SectionId { get; set; }

        [Required]
        [Display(Name = "участок")]
        public Section Section { get; set; }

        [Required]
        [Display(Name = "idцеха")]
        public int DepartmentId { get; set; }

    }
}
