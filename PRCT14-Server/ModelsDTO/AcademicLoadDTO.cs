using PRCT14_Server.Models;

namespace PRCT14_Server.Models
{    

    public partial class AcademicLoadDTO
    {
        public int AcademLoadCode { get; set; }

        public string TeacherSurname { get; set; }

        public string DisciplineName { get; set; }

        public int Group { get; set; }

        public int Semester { get; set; }

        public int NumberOfHours { get; set; }

        public AcademicLoadDTO() { }

        public AcademicLoadDTO(AcademicLoad value) {
            AcademLoadCode = value.AcademLoadCode;
            TeacherSurname = value.TeacherCodeNavigation.Surname;
            DisciplineName = value.DisciplineCodeNavigation.Name;
            Group = value.Group;
            Semester = value.Semester;
            NumberOfHours = value.NumberOfHours;

        }       
    }
}
