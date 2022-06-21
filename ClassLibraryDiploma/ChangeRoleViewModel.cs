using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class ChangeRoleViewModel
    {
        public string UserId { get; set; }

        public Robotnik robotnik { get; set; }
        public string Roles { get; set; }
        public List <Robotnik> AllRobotnik { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public ChangeRoleViewModel Change { get; set; }
        public List<ChangeRoleViewModel> ChangeRoleView { get; set; }
        public IList<string> RobotnikRoles { get; set; }

        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            RobotnikRoles = new List<string>();
            AllRobotnik = new List<Robotnik>();
            ChangeRoleView = new List<ChangeRoleViewModel>();


        }
    }
}
