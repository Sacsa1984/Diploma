using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class RegisterViewModelWorkihg
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Номер заказа")]
        public int NumZakaz { get; set; }
        [Required]
        [Display(Name = "Чертёж")]
        public string NameСhertezh { get; set; }
        [Required]
        [Display(Name = "Стоимость изготовления")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Потраченое время на выполнение")]
        public string LeadTime { get; set; }

        [Required]
        [Display(Name = "Стоимость изготовления")]
        public int Saiz { get; set; }
        [Required]
        [Display(Name = "Заметки или требования")]
        public string Notice { get; set; }
        [Required]
        [Display(Name = "Заметки или требования")]
        public int Sostoynie { get; set; }
        [Required]
        [Display(Name = "ЦЕХ")]
        public int DepartmentId { get; set; }

        public List<int> Equips { get; set; }

    }
}
