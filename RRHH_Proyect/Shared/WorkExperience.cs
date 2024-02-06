using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRHH_Proyect.Shared
{
    public class WorkExperience
    {
        public int Id {  get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Salary {  get; set; }
        public bool IsActive {  get; set; }
    }
}
