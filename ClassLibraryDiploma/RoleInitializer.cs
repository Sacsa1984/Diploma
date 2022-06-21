using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibraryDiploma
{
  public  class RoleInitializer
    {
    
        public static async Task InitializeAsync(UserManager<Robotnik> robotnikManager, RoleManager<IdentityRole> roleManager)
        {
            
            string adminEmail = "Admin";
            string password ="00000000";
            if (await roleManager.FindByNameAsync("Админ") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Админ"));
            }
            if (await roleManager.FindByNameAsync("Робочий") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Робочий"));
            }
            if (await roleManager.FindByNameAsync("Распределитель работ") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Распределитель работ"));
            }
            if (await robotnikManager.FindByNameAsync(adminEmail) == null)
            {
                Robotnik admin = new Robotnik { Name = adminEmail, UserName = password };
               
                IdentityResult result = await robotnikManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await robotnikManager.AddToRoleAsync(admin, "Админ");
                }
                
            }
        }
    }
}
