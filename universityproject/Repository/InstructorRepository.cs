using ContosoUniversity.Data;
using ContosoUniversity.IRepository;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolContext _context;

        public InstructorRepository(SchoolContext context) { _context = context;}
        public async Task<List<Instructor>> getRelatedStudentsAndOfficeAssignments()
        {
            return await _context.Instructors
                          .Include(i => i.OfficeAssignment)
                          .Include(i => i.CourseAssignments)
                            .ThenInclude(i => i.Course)
                                .ThenInclude(i => i.Enrollments)
                                    .ThenInclude(i => i.Student)
                          .Include(i => i.CourseAssignments)
                            .ThenInclude(i => i.Course)
                                .ThenInclude(i => i.Department)
                          .AsNoTracking()
                          .OrderBy(i => i.LastName)
                          .ToListAsync();
        }
    }
}
