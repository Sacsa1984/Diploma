using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class Role 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Robotnik> Users { get; set; }
        public Role()
        {
            Users = new List<Robotnik>();
        }
    }
}
