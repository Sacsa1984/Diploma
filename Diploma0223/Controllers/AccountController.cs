using ClassLibraryDiploma;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace Diploma0223.Controllers
{
    
    public class AccountController : Controller
    {

     


        private readonly UserManager<Robotnik> _robotnikManager;
        private readonly SignInManager<Robotnik> _robotniksignInManager;

        private readonly ApplicationContext context;

        RoleManager<IdentityRole> _roleManager;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<Robotnik> robotnikManager, SignInManager<Robotnik> signInManager, ApplicationContext context)
        {
            _roleManager = roleManager;
            _robotnikManager = robotnikManager;
            _robotniksignInManager = signInManager;
            this.context = context;

            if (!context.Departments.Any())
            {
                context.Departments.AddRange(new Department { Name = "Цех 01", IdDepartment = 001 },
                new Department { Name = "Цех02", IdDepartment = 002 });
                context.SaveChanges();
                context.Sections.AddRange(
                     new Section { Name = "Участок 01", DepartmentId = 1 },
                new Section { Name = "Участок 02", DepartmentId = 1 },
                new Section { Name = "Участок 03", DepartmentId = 2 },
                new Section { Name = "Участок 04", DepartmentId = 2 });
                context.SaveChanges();
                context.Equipments.AddRange(
                    new Equipment { Name = "Оборудование 01", INN = "35", Characteristics = "MAX D=3200, L2000", SectionId = 1, DepartmentId = 1 },
                    new Equipment { Name = "Оборудование 02", INN = "12", Characteristics = "MAX D=3200, L2400", SectionId = 2, DepartmentId = 2 });
                context.SaveChanges();
                context.Workings.AddRange(new Working { Name = "Задание", NameСhertezh = "чертёж", LeadTime = "время выполнения", NumZakaz = 000000, Price = 0, Saiz = 0, Notice = "Заметки", Sostoynie = 0, Path = "nn" });
                context.SaveChanges();


            }

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            Robotnik user = await context.Robotnik.FirstOrDefaultAsync(u =>u.Name==model.Password && u.UserName == model.Name);
            if (ModelState.IsValid)
            {


               

                if (user != null)
                {

                    await _robotniksignInManager.PasswordSignInAsync(model.Name, model.Password, isPersistent: false, false);

                    var userRoles = await _robotnikManager.GetRolesAsync(user);


                    if (userRoles[0].Equals("Админ"))
                    {
                    
                       
                       // ClaimsPrincipal claims = new ClaimsPrincipal();
                       

                        //await Authenticate(model.Name); // аутентификация





                        return RedirectToAction("IndexAdmin", "Robotnik");

                    }
                    if (userRoles[0].Equals("Распределитель работ"))
                    {

                        

                        return RedirectToAction("IndexRospred", "Robotnik");

                    }
                    if (userRoles[0].Equals("Робочий"))
                    {
                        RobotnicEquipmentWorkimgViwe robotnicEquipmentWorkimg = new RobotnicEquipmentWorkimgViwe();


                        Working working = await context.Workings.FirstOrDefaultAsync(u => u.Oborudovaniye == user.Oborudovaniye);
                        Equipment equipment = await context.Equipments.FirstOrDefaultAsync(i => i.Id == user.Oborudovaniye);
                        robotnicEquipmentWorkimg.working = working;
                        robotnicEquipmentWorkimg.robotnik = user;
                        robotnicEquipmentWorkimg.equipment = equipment;



                        return View("IndexRobotnik", robotnicEquipmentWorkimg);

                    }
                    // return RedirectToAction("Index", "Home");

                }

               

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                
               
               
            }

           
            return View();
        }
        //Аунтетификация !!!!!!
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,userName),

            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //[Authorize(Roles = "Админ")]
        [HttpGet]
        public async Task<IActionResult> RegisterRobotnik()
        {
            var deptmnts = await context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(deptmnts, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterRobotnik(RegisterViewModelRobotnik model)
        {
            if (ModelState.IsValid)
            {
                Robotnik robotnik = new Robotnik
                { UserName=model.TabNum, Name = model.Name,
                    NameHuman=model.NameHuman,
                    Otchestvo = model.Otchestvo,
                    Surname = model.Surname,
                    Rozryad = model.Rozryad,
                    Brigade=model.Brigade
                    

                };
                // добавляем пользователя
                var result = await _robotnikManager.CreateAsync(robotnik, model.TabNum);
                
                if (model.Equips!=null)
                {
                    foreach (int equipId in model.Equips)
                    {

                        Equipment equip = await context.Equipments.FindAsync(equipId);
                        robotnik.Oborudovaniye = equip.Id;
                        if (equip != null)
                            robotnik.Equipments.Add(equip);
                    }
                }
                await context.SaveChangesAsync();


                FunctionServis function = new FunctionServis();//своя функция с библиотеки ищет совпадение роли в списке ролей 
                string Rol = function.PoskRole(model, _roleManager);


                if (Rol != null)
                {
                    await _robotnikManager.AddToRoleAsync(robotnik, Rol);
                }
                else
                {
                    return RedirectToAction("ErorPravaDoctup", "Home");
                }

                if (result.Succeeded)
                {
                    // установка куки
                    await _robotniksignInManager.SignInAsync(robotnik, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> RegisterRospred()
        {
            var deptmnts = await context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(deptmnts, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterRospred(RegisterViewModelRospred model)
        {
            if (ModelState.IsValid)
            {
                Robotnik robotnikRospred = new Robotnik {
                    UserName = model.TabNum,
                    Name = model.Name,
                    NameHuman = model.NameHuman,
                    Otchestvo = model.Otchestvo, Surname = model.Surname,
                    Rozryad = model.Rozryad,
                };
                // добавляем пользователя
                var result1 = await _robotnikManager.CreateAsync(robotnikRospred, model.TabNum);
                FunctionServis function = new FunctionServis();//своя функция с библиотеки ищет совпадение роли в списке ролей 
                string Rol = function.PoskRole(model, _roleManager);


                if (Rol != null)
                {
                    await _robotnikManager.AddToRoleAsync(robotnikRospred, Rol);
                }
                else
                {
                    return RedirectToAction("ErorPravaDoctup", "Home");
                }
                if (result1.Succeeded)
                {

                    // установка куки
                    await _robotniksignInManager.SignInAsync(robotnikRospred, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result1.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction("RobotnikListGet", "Robotnik"); 
        }

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModelAdmin model)
        {

            if (ModelState.IsValid)
            {
                Robotnik robotnikAdmin = new Robotnik 
                {
                    UserName = model.TabNum,
                    Name = model.Name,
                    NameHuman = model.NameHuman,
                    Otchestvo = model.Otchestvo,
                    Surname = model.Surname };


                // добавляем пользователя
                var result1 = await _robotnikManager.CreateAsync(robotnikAdmin, model.TabNum);

                FunctionServis function = new FunctionServis();//своя функция с библиотеки ищет совпадение роли в списке ролей 
                string Rol = function.PoskRole(model, _roleManager);


                if (Rol != null)
                { await _robotnikManager.AddToRoleAsync(robotnikAdmin, Rol);
                }
                else
                {
                    return RedirectToAction("ErorPravaDoctup", "Home");
                }


                if (result1.Succeeded)
                {

                    // установка куки
                    await _robotniksignInManager.SignInAsync(robotnikAdmin, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result1.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction("RobotnikListGet", "Robotnik");
        }
        [HttpGet]
        //[ChildActionsOnly]
        public async Task<IActionResult> GetSections(int id)
        {
            Department department = await context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();
            await context.Entry(department).Collection(t => t.Sections).LoadAsync();
            var sections = department.Sections.ToList();
            ViewBag.Sections = new SelectList(sections, "Id", "Name");
            return View("_GetSections");
        }

        [HttpGet]
        public async Task<IActionResult> getEquipment(int id)
        {
            Section section = await context.Sections.FindAsync(id);
            if (section == null)
                return NotFound();
            await context.Entry(section).Collection(t => t.Equipments).LoadAsync();
            var equipment = section.Equipments.ToList();
            //ViewBag.Equipment = new SelectList(equipment, "Id", "Name");
            return View("_GetEquipment", equipment);
        }

        
    }
}

