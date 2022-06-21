using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
  public  class RegisterViewModelRobotnik
    {
        [Required]
        [Display(Name = "Password ")]
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
        [Display(Name = "Участок")]
        public int SectionId { get; set; }

        [Required]
        [Display(Name = "Розряд")]
        public string Rozryad { get; set; }
        [Required]
        [Display(Name = "Номер бригады ")]
        public int Brigade { get; set; }

        //[Required]
        //[Display(Name = "Оборудование ")]
        //public string Oborudovaniye { get; set; }
        [Required]
        [Display(Name ="ЦЕХ")]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        public List<int> Equips { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Подтвердить пароль")]

        //public string PasswordConfirm { get; set; }
    }
}
