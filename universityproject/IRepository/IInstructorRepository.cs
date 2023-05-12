using ContosoUniversity.Models;

namespace ContosoUniversity.IRepository
{
    public interface IInstructorRepository
    {
        public Task<List<Instructor>> getRelatedStudentsAndOfficeAssignments();
    }
}
