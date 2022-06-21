using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
   public class DepartmentSectionEquipment
    {
        public Section sections { get; set; }
        public Department departments { get; set; }
        public Equipment equipment { get; set; }
        public Robotnik robotnik { get; set; }
        public Working working { get; set; }

    }
}
