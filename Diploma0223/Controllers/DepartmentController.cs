using ClassLibraryDiploma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma0223.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly ApplicationContext context;
       
        public DepartmentController(ApplicationContext context)
        {

            this.context = context;
        }
       
        [HttpGet]
        public IActionResult RegisterDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDepartment(RegisterViewModelDepartment model)
        {
            
            
                Department department = new Department { Name = model.Name, 
                    IdDepartment=model.IdDepartment };

            await context.Departments.AddAsync(department);

            context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        public IActionResult DepartmentList() => View(context.Departments.ToList());

       
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)

        {
            if (id != null)
            {
                Department department = await context.Departments.FirstOrDefaultAsync(p => p.Id == id);
            context.Entry(department).State = EntityState.Deleted;
            context.SaveChanges();
                if (department != null)
                {
                    return RedirectToAction("DepartmentList", "Department");
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Department department = await context.Departments.FirstOrDefaultAsync(p => p.Id == id);
                if (department != null)
                    return View(department);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            context.Departments.Update(department);
            await context.SaveChangesAsync();
            return RedirectToAction("DepartmentList", "Department");
        }


    }
}
