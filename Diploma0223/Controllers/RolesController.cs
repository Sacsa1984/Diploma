using ClassLibraryDiploma;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma0223.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<Robotnik> _robotManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<Robotnik> robotManager)
        {
            _roleManager = roleManager;
            _robotManager = robotManager;
        }
            public IActionResult Index() => View(_roleManager.Roles.ToList());

            public IActionResult Create() => View();

            [HttpPost]
            public async Task<IActionResult> Create(string name)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return View(name);
            }

            [HttpPost]
            public async Task<IActionResult> Delete(string id)
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                }
                return RedirectToAction("Index");
            }

            public IActionResult UserList() => View(_robotManager.Users.ToList());

            public async Task<IActionResult> Edit(string userId)
            {
                // получаем пользователя
                Robotnik robotnik = await _robotManager.FindByIdAsync(userId);
                if (robotnik != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _robotManager.GetRolesAsync(robotnik);
                    var allRoles = _roleManager.Roles.ToList();
                
                    ChangeRoleViewModel model = new ChangeRoleViewModel
                    {
               
                        robotnik= robotnik,
                        RobotnikRoles = userRoles,
                        AllRoles = allRoles
                    };
                    return View(model);
                }

                return NotFound();
            }
            [HttpPost]
            public async Task<IActionResult> Edit(string userId, List<string> roles)
            {
                // получаем пользователя
                Robotnik robotnik = await _robotManager.FindByIdAsync(userId);
                if (robotnik != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _robotManager.GetRolesAsync(robotnik);
                    // получаем все роли
                    var allRoles = _roleManager.Roles.ToList();
                    // получаем список ролей, которые были добавлены
                    var addedRoles = roles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(roles);

                    await _robotManager.AddToRolesAsync(robotnik, addedRoles);

                    await _robotManager.RemoveFromRolesAsync(robotnik, removedRoles);

                    return RedirectToAction("UserList");
                }

                return NotFound();
            }
        
 }   }

   
