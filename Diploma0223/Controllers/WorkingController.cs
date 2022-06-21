using ClassLibraryDiploma;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diploma0223.Controllers
{
    public class WorkingController : Controller
    {
        private readonly ApplicationContext context;
        IWebHostEnvironment _appEnvironment;

        public WorkingController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {

            this.context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterWorking()
        {
            var deptmnts = await context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(deptmnts, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterWorking(RegisterViewModelWorkihg model, IFormFile uploadedFile)
        { 

            if (model!=null)
            {
                if (uploadedFile != null)
                {
                    // путь к папке Files
                    string path = "/Files/" +uploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    //FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                    //_context.Files.Add(file);
                    //_context.SaveChanges();
               

                Working working = new Working
                {
                    Name = model.Name,
                    NumZakaz = model.NumZakaz,
                    NameСhertezh =uploadedFile.FileName,
                    Path= path,
                    LeadTime = model.LeadTime,
                    Saiz = model.Saiz,
                    Price = model.Price,
                    Notice = model.Notice,
                    Sostoynie=model.Sostoynie,
                    

                };
                    foreach (int equipId in model.Equips)
                    {

                       // Equipment equip = await context.Equipments.FindAsync(model.Equips);
                        working.Oborudovaniye = equipId;
                    }

                   



                      // добавляем пользователя
                    await context.Workings.AddAsync(working);



                context.SaveChanges();
                }

            }
            return RedirectToAction("WorkingsList", "Working");

        }

        public IActionResult WorkingsList()
        {
            //////////////////////////////////////////////
            var work = context.Workings.ToList();
            var equip = context.Equipments.ToList();
            
            List<WorkingEquipment> workingEquipments = new List<WorkingEquipment>();


            foreach (var item in work)
            {
                WorkingEquipment working = new WorkingEquipment();
                working.workingObj = item;
                foreach (var itemEquip in equip)
                {
                    if (item.Oborudovaniye == itemEquip.Id)
                    {
                        working.equipmentObj = itemEquip;
                    }
                }
                workingEquipments.Add(working);
                


            }
            //WorkingEquipment workingEquipment = new WorkingEquipment
            //{
            //    working = work,
              

            //};



            //foreach (var item in workingEquipment.working)
            //{
            //    Equipment equip = await context.Equipments.FindAsync(item.Oborudovaniye);
            //    workingEquipment.working.Add;

            //}

            //workingEquipment.obor = await context.Equipments.FindAsync(workingEquipment.working)

            return View(workingEquipments);
        }
        

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)

        {
            if (id != null)
            {
                Working working = await context.Workings.FirstOrDefaultAsync(p => p.Id == id);
                context.Entry(working).State = EntityState.Deleted;
                context.SaveChanges();
                if (working != null)
                {
                    return RedirectToAction("WorkingsList", "Working");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Working working = await context.Workings.FirstOrDefaultAsync(p => p.Id == id);
                if (working != null)
                    return View(working);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Working working)
        {
            string path = "/Files/" + working.NameСhertezh;

            working.Path = path;
            context.Workings.Update(working);
            await context.SaveChangesAsync();
            return RedirectToAction("WorkingsList", "Working");
        }
    }
}
