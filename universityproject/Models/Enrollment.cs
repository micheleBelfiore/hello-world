using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }//foreign key (FK), and the corresponding navigation property is Course
        public int StudentID { get; set; }//foreign key (FK), and the corresponding navigation property is Student
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; }// navigation properties
        public Student Student { get; set; }// navigation properties
    }
}
