using ClassLibraryDiploma;
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
    public class RobotnikController : Controller
    {
        private readonly ApplicationContext context;
        UserManager<Robotnik> _robotManager;

        public RobotnikController(ApplicationContext context, UserManager<Robotnik> robotManager)
        {

            this.context = context;
            _robotManager = robotManager;


        }

       // public IActionResult RobotnikListGet() => View(context.Robotnik.ToList());
        public async Task <IActionResult> RobotnikListGet()
        {
            var robotnik = context.Users.ToList();

            ChangeRoleViewModel model = new ChangeRoleViewModel
            {

                AllRobotnik = robotnik,
                

                  };
            foreach (var item in robotnik)
            {
                
              var userRoles = await _robotManager.GetRolesAsync(item);
                model.RobotnikRoles=userRoles;

            }
            //var role = context.Roles.ToList();

            return View(model);
        }

        public async Task<IActionResult> ProizvodkListGet()
        {
            

            List<RobotnicEquipmentView> robotnicEquipments = new List<RobotnicEquipmentView>();
            var userRoles1 = await _robotManager.GetUsersInRoleAsync("Робочий");
            var Equipments = context.Equipments.ToList();
            foreach (var item in userRoles1)
            {
                RobotnicEquipmentView robotnicEquipmentView = new RobotnicEquipmentView();
                robotnicEquipmentView.robotnik = item;
                foreach (var equ in Equipments)
                {
                    if(robotnicEquipmentView.robotnik.Oborudovaniye== equ.Id)
                    {
                        robotnicEquipmentView.equipment = equ;
                    }
                    
                }
                robotnicEquipments.Add(robotnicEquipmentView);

            }


            return View(robotnicEquipments);
        }

        [HttpGet]
        public async Task<IActionResult> ProizvodDeteils(string id)
        {
            if (id != null)
            {
                
                DepartmentSectionEquipment departmentSectionEquipment = new DepartmentSectionEquipment();
               

                departmentSectionEquipment.robotnik = await context.Robotnik.FirstOrDefaultAsync(p => p.Id == id);
                if (departmentSectionEquipment != null)
                {
                    departmentSectionEquipment.equipment = await context.Equipments.FindAsync(departmentSectionEquipment.robotnik.Oborudovaniye);
                    departmentSectionEquipment.working = await context.Workings.FirstOrDefaultAsync(p => p.Oborudovaniye == departmentSectionEquipment.equipment.Id);
                }
                return View(departmentSectionEquipment);
            }
            return NotFound();



        }
       

        [HttpGet]
        public async Task<IActionResult> Delete(string id)

        {
            if (id != null)
            {
              Robotnik  robotnik = await context.Robotnik.FirstOrDefaultAsync(p => p.Id == id);
                context.Entry(robotnik).State = EntityState.Deleted;
                context.SaveChanges();
                if (robotnik != null)
                {
                    return RedirectToAction("RobotnikListGet", "Robotnik");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id != null)
            {
              Robotnik  robotnik = await context.Robotnik.FirstOrDefaultAsync(p => p.Id == id);
                if (robotnik != null)
                    return View(robotnik);
            }
            return NotFound();

            

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Robotnik robotnik, string id)
        {
            //Robotnik robotnik1 = new Robotnik();
            //robotnik1 = robotnik;
            Robotnik robotnikNew = await _robotManager.FindByIdAsync(id);
            // Robotnik rob = await _robotManager.FindByIdAsync(robotnik.Id);
            //robotnikNew.Name = robotnik.Name;
           
            robotnikNew.NameHuman = robotnik.NameHuman;
            robotnikNew.Otchestvo = robotnik.Otchestvo;
            robotnikNew.Surname = robotnik.Surname;
            robotnikNew.Rozryad = robotnik.Rozryad;


            //////....
            //context.Robotnik.Update(robotnik1);
            await _robotManager.UpdateAsync(robotnikNew);
            
            return RedirectToAction("RobotnikListGet", "Robotnik");
        }
        public IActionResult IndexAdmin()
        {
            
            return View();
        }
        public IActionResult IndexRobotnik(Robotnik robotnik)
        {
            


            return View();
        }
        public IActionResult IndexRospred()
        {
            return View();
        }

        public IActionResult ReposttList() => View(context.Reports.ToList());


        [HttpGet]
        public async Task<IActionResult> ReposttDelete(int? id)

        {
            if (id != null)
            {
                Report report = await context.Reports.FirstOrDefaultAsync(p => p.id == id);
                context.Entry(report).State = EntityState.Deleted;
                context.SaveChanges();
                if (report != null)
                {
                    return RedirectToAction("ReposttList", "Robotnik");
                }
            }
            return NotFound();
        }
    }
}
