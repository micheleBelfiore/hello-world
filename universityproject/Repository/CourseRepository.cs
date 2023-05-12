using ContosoUniversity.Data;
using ContosoUniversity.IRepository;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context) { _context = context; }
        public async Task<List<Course>> getRelatedDepartment()
        {
            return await _context.Courses.Include(c => c.Department).AsNoTracking().ToListAsync<Course>(); 
        }
    }
}
