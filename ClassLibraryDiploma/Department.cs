using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
    public class Department
    {
        public int Id { get; set; }

       public int IdDepartment { get; set; }
        public string Name { get; set; }

        public List<Section> Sections { get; set; }
    }
}
