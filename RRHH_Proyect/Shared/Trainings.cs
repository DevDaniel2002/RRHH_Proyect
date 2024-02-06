using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRHH_Proyect.Shared
{
    public class Trainings
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Institution { get; set; }
        public bool IsActive { get; set; }

        public int TrainingTypeId { get; set; }
        public TrainingTypes TrainingType { get; set; }
    }
}
