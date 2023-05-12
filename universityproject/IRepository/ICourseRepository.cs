using ContosoUniversity.Models;

namespace ContosoUniversity.IRepository
{
    public interface ICourseRepository
    {
        public Task<List<Course>> getRelatedDepartment();
    }
}
