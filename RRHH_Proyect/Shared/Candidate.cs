using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRHH_Proyect.Shared
{
    public class Candidate
    {
        public int Id { get; set; }
        public int Cedula { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //public int DepartmentId { get; set; }
        //public int DesiredPosition { get; set; }
        public int DesiredSalary { get; set; }
        public int TrainingId { get; set; }
        public int WorkExperienceId { get; set; }
        public int LanguagueId { get; set; }
        public string RecommendFor { get; set; }
        public bool IsActive { get; set; }

        //public Department Department { get; set; }
        //public Position DesiredPositionNavigation { get; set; }
        public Language Language { get; set; }
        public ICollection<Trainings> Trainings { get; set; }
        public WorkExperience WorkExperience { get; set; }
    }
}
