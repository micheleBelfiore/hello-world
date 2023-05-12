using ContosoUniversity.Models;

namespace ContosoUniversity.IRepository
{
    public interface IStudentRepository
    {
        public Task<Student> GetStudentIncludeEnrollmentsIncludeCourse(int id);
    }
}
