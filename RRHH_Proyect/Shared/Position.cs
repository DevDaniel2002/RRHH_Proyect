﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRHH_Proyect.Shared
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        //public int DepartmentId { get; set; }
        //public Department Department { get; set; }
    }
}