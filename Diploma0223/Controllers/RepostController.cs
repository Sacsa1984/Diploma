using ClassLibraryDiploma;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Diploma0223.Controllers
{
    public class RepostController : Controller
    {
        private readonly ApplicationContext context;
        public RepostController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> AddReports(RobotnicEquipmentWorkimgViwe dan)
        {
           Robotnik user = await context.Robotnik.FirstOrDefaultAsync(m => m.Id == dan.robotnik.Id);
            if (user!=null)
            {

         
            Report report = new Report

            { 
            NameZada=dan.working.Name,
        
           Price=dan.working.Price,
           LeadTime=dan.working.LeadTime, 
           Saiz=dan.working.Saiz,
           Notice=dan.working.Notice,
           Sostoynie=dan.working.Sostoynie,
           NameHuman=user.NameHuman,
                TabNum = user.UserName,
                Otchestvo=user.Otchestvo,
           Surname =user.Surname,
           Rozryad =user.Rozryad,
           Brigade=user.Brigade,
           NameOborud=dan.equipment.Name,
            INN=dan.equipment.INN,
              };
            await context.Reports.AddAsync(report);

            context.SaveChanges();
            }
            return RedirectToAction("IndexRobotnik", "Robotnik");
        }
          

           
    }

    
}
