
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
  public  class FunctionServis
    {
        
        
            public string PoskRole(RegisterViewModelAdmin model, RoleManager<IdentityRole> roleManager)
            {
                var allRoles = roleManager.Roles.ToList();
                var Role = model.Role;

                foreach (var item in allRoles)
                {
                    if (item.ToString().Equals(Role))
                    {

                        return Role;
                    }

                }
                return null;
            }
        public string PoskRole(RegisterViewModelRospred model, RoleManager<IdentityRole> roleManager)
        {
            var allRoles = roleManager.Roles.ToList();
            var Role = model.Role;

            foreach (var item in allRoles)
            {
                if (item.ToString().Equals(Role))
                {

                    return Role;
                }

            }
            return null;
        }
        public string PoskRole(RegisterViewModelRobotnik model, RoleManager<IdentityRole> roleManager)
        {
            var allRoles = roleManager.Roles.ToList();
            var Role = model.Role;

            foreach (var item in allRoles)
            {
                if (item.ToString().Equals(Role))
                {

                    return Role;
                }

            }
            return null;
        }

        
    }
    
}
