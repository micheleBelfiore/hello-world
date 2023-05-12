using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.IRepository;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorsController(IInstructorRepository instructorRepository) 
        {
            _instructorRepository = instructorRepository;
        }
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            InstructorIndexData viewModel = new InstructorIndexData();
            viewModel.Instructors = await _instructorRepository.getRelatedStudentsAndOfficeAssignments();

            if(id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructors.Where(i => i.ID == id.Value).Single();
                viewModel.Courses = instructor.CourseAssignments.Select(i => i.Course);
            }

            if(courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;

                viewModel.Enrollments = viewModel.Courses.Where(c => c.CourseID == courseID.Value)
                    .Single()
                    .Enrollments;
            }
            return View(viewModel);
        }
    }
}
