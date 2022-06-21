using ClassLibraryDiploma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma0223.Controllers
{
    public class SectionController : Controller
    {
        private readonly ApplicationContext context;
     
        public SectionController(ApplicationContext context)
        {

            this.context = context;
        }

        [HttpGet]
        public async Task <IActionResult> RegisterSection()
        {
            var deptmnts = await context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(deptmnts, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterSection(RegisterViewModelSection model)
        {


            Section section = new Section
            {
                Name = model.Name,
                 DepartmentId = model.DepartmentId
            };

            await context.Sections.AddAsync(section);

            context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> SectionList()
        {
            List<Department> listdepartment;
            listdepartment = await context.Departments.ToListAsync();

            List<Section> listsections;
            listsections = await context.Sections.ToListAsync();
            ListDepartmentSectionEquipment list = new ListDepartmentSectionEquipment();
            list.Sections = listsections;
            list.departments = listdepartment;

            return View(list);
        }

        public async Task<IActionResult> SectionList_Rospred()
        {
            List<Department> listdepartment;
            listdepartment = await context.Departments.ToListAsync();

            List<Section> listsections;
            listsections = await context.Sections.ToListAsync();
            ListDepartmentSectionEquipment list = new ListDepartmentSectionEquipment();
            list.Sections = listsections;
            list.departments = listdepartment;

            return View(list);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)

        {
            if (id != null)
            {
                Section section = await context.Sections.FirstOrDefaultAsync(p => p.Id == id);
                context.Entry(section).State = EntityState.Deleted;
                context.SaveChanges();
                if (section != null)
                {
                    return RedirectToAction("SectionList", "Section");
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
                Section section = await context.Sections.FirstOrDefaultAsync(p => p.Id == id);
                if (section != null)
                {
                    
                    Department department = await context.Departments.FirstOrDefaultAsync(p => p.Id == section.DepartmentId);
                    departmentSectionEquipment.sections = section;
                    departmentSectionEquipment.departments = department;
                    return View(departmentSectionEquipment);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentSectionEquipment departmentSectionEquipment)
        {
            Section section = new Section();
            section = departmentSectionEquipment.sections;


            context.Sections.Update(section);
            await context.SaveChangesAsync();
            
            return RedirectToAction("SectionList", "Section");
        }
    }
}
