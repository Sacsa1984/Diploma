using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDiploma
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string INN { get; set; }
        public string Characteristics { get; set; }
        // public DateTime Characteristics { get; set; }
        [ForeignKey(nameof(Section))]
        public int SectionId { get; set; }
        public int DepartmentId { get; set; }
        public Section Section { get; set; }
        public List<Robotnik> Workers { get; set; }
        


    }
}
