using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
  public class Robotnik : IdentityUser
    {
     public Robotnik()
        {
            Equipments  = new List<Equipment>();
        }
        public string Name { get; set; }
        public string NameHuman { get; set; }
        public string Otchestvo { get; set; }
        public string Surname { get; set; }

        public int IdWorking { get; set; }
        public string Rozryad { get; set; }
        public int   Brigade { get; set; }
        //public int? RoleId { get; set; }
        //public Role Role { get; set; }
        public int Oborudovaniye { get; set; }
        public List<Equipment> Equipments { get; set; }
        
    }
}
