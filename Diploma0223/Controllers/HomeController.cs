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
    public class HomeController : Controller
    {
        
      
        private readonly ApplicationContext context;
        private readonly UserManager<Robotnik> _robotnikManager;


        public HomeController( ApplicationContext context, UserManager<Robotnik> robotManager)
        {
          
            this.context = context;

            
            _robotnikManager = robotManager;
           
           
           

        }


       
        public IActionResult Index( )
        {


            return RedirectToAction("IndexAdmin", "Robotnik");

        }
        public IActionResult ErorPravaDoctup()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        
    }
}
