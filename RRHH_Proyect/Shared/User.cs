using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRHH_Proyect.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

    }

    public class ParentClass
    {
        public int Id { get; set; }
        public string ParentName { get; set; }
        public ChildClass Child { get; set; }
    }

    public class ChildClass
    {
        public int Id { get; set; }
        public string ChildName { get; set; }
    }
}