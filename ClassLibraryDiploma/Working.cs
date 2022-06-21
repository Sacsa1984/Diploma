using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class Working
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumZakaz { get; set; }
        public string NameСhertezh { get; set; }
        public string Path { get; set; }
       
        public float Price { get; set; }
        public string LeadTime { get; set; }
        public int Saiz { get; set; }
        public string Notice { get; set; }
        public int Sostoynie { get; set; }
        public int Oborudovaniye { get; set; }
        public Equipment equipment{ get; set; }
        //public List<int> IdWorking { get; set; }

    }
}
