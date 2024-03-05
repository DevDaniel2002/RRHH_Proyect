using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRHH_Proyect.Shared
{
    public class Employee
    {
        public int Id { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime HiringDate{ get; set; }
        public Department Department { get; set; }
        public Position Position { get; set; }
        public double Salary { get; set; }
        public bool IsActive { get; set; }
    }
}
