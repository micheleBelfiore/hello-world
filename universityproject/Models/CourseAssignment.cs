namespace ContosoUniversity.Models
{
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public int CourseID { get; set; }//foreign key
        public Instructor Instructor { get; set; }//navigation property
        public Course Course { get; set; }//navigation property
    }
}
