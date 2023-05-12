using ContosoUniversity.Data;
using ContosoUniversity.IRepository;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private SchoolContext _context;
        public StudentRepository(SchoolContext context) {
            _context = context;
        }
        public async Task<Student> GetStudentIncludeEnrollmentsIncludeCourse(int id)
        {
         var student = await _context.Students
                .Include(e => e.Enrollments)
                .ThenInclude(c => c.Course)
                .AsNoTracking()//improves performance in scenarios where the entities returned won't be updated in the current context's lifetime
                .FirstOrDefaultAsync(s =>s.ID == id);
            if (student == null)
            {
                throw new NotImplementedException($"nothing entity find with this id {id}");
            }
            return student;
        }
    }
}
