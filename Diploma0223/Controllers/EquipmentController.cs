using ClassLibraryDiploma;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma0223.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly ApplicationContext context;
        public EquipmentController(ApplicationContext context)
        {

            this.context = context;
        }

        public async Task<IActionResult> EquipmentList()
        {
            
            
            List<Department> listdepartment;
            listdepartment = await context.Departments.ToListAsync();

            List<Section> listsections;
            listsections = await context.Sections.ToListAsync();
            List<Equipment> listequipment;
            listequipment= await context.Equipments.ToListAsync();
            ListDepartmentSectionEquipment list = new ListDepartmentSectionEquipment();
            list.Sections = listsections;
            list.departments = listdepartment;
            list.equipment = listequipment;
           
            return View(list);
        }

        public async Task<IActionResult> EquipmentList_Rospred()
        {


            List<Department> listdepartment;
            listdepartment = await context.Departments.ToListAsync();

            List<Section> listsections;
            listsections = await context.Sections.ToListAsync();
            List<Equipment> listequipment;
            listequipment = await context.Equipments.ToListAsync();
            ListDepartmentSectionEquipment list = new ListDepartmentSectionEquipment();
            list.Sections = listsections;
            list.departments = listdepartment;
            list.equipment = listequipment;

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> RegisterEquipment()
        {
           

            var deptmnts = await context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(deptmnts, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterEquipment(RegisterViewModelEquipment model)
        {


            Equipment equipment = new Equipment
            {
                Name = model.Name,
                 INN=model.INN,
                  Characteristics=model.Characteristics,
                   SectionId=model.SectionId,
                   DepartmentId=model.DepartmentId
                   

            };

            await context.Equipments.AddAsync(equipment);

            context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)

        {
            if (id != null)
            {
                Equipment equipment = await context.Equipments.FirstOrDefaultAsync(p => p.Id == id);
                context.Entry(equipment).State = EntityState.Deleted;
                context.SaveChanges();
                if (equipment != null)
                {
                    return RedirectToAction("EquipmentList", "Equipment");
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            DepartmentSectionEquipment departmentSectionEquipment = new DepartmentSectionEquipment();
            if (id != null)
            {
                Equipment equipment= await context.Equipments.FirstOrDefaultAsync(p => p.Id == id);
                if (equipment != null)
                {
                    Section section = await context.Sections.FirstOrDefaultAsync(p => p.Id == id);
                    if (section != null)
                    {

                        Department department = await context.Departments.FirstOrDefaultAsync(p => p.Id == section.DepartmentId);
                        departmentSectionEquipment.sections = section;
                        departmentSectionEquipment.departments = department;
                        departmentSectionEquipment.equipment = equipment;
                        return View(departmentSectionEquipment);
                    }
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentSectionEquipment departmentSectionEquipment)
        {
            Equipment equipment = new Equipment();
            equipment = departmentSectionEquipment.equipment;


            context.Equipments.Update(equipment);
            
            await context.SaveChangesAsync();

            return RedirectToAction("EquipmentList", "Equipment");
        }

    }
}
