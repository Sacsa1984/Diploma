using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
    public class ApplicationContext : IdentityDbContext<Robotnik>
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Robotnik> Robotnik { get; set; }
        public DbSet<Working> Workings { get; set; }
        public DbSet<Report> Reports { get; set; }



        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
           //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

       
    }
}
