using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ContosoUniversity.IUnitOfWork;
using ContosoUniversity.Models;
using ContosoUniversity.IRepository;
using ContosoUniversity.Services;
using ContosoUniversity.UnitOfWork;
using System.Linq;
using ContosoUniversity.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace ContosoUniversity.Controllers
{
    

    public class StudentsController : Controller
    {
        private readonly InterfaceUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentReopsitory;
        private readonly ILogger<StudentsController> _logger;
        // [BindProperty(SupportsGet = true)]
        // public string name { get; set; }


        public StudentsController(InterfaceUnitOfWork unitOfWork, IStudentRepository studentReopsitory, ILogger<StudentsController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _studentReopsitory = studentReopsitory;
        }

        //Prova
        //GET
        [HttpGet]

        public async Task<string> Prova( string name)
        {
            
             await _unitOfWork.CreateMyTempTableAsync(true);
            List<int> listIds = new List<int>();
            listIds.Add(1);
            listIds.Add(2);

            listIds.Add(3);

            await _unitOfWork.InsertMyTempTableAsync(listIds);
    
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var instructor = await _unitOfWork.getInstructorRepository.GetAll()
                        .Where(p => _unitOfWork.GetMyTempTable().Contains(p.ID))
                        .ToListAsync();
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            //   var x = await _unitOfWork.DeleteMyTempTableAsync();
             var tempTable = _unitOfWork.GetMyTempTable();

            return $"id : {0}\t name : {name}BELFIOREmICHELE";
        }
       
        

        // GET: Students
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var studentList = from s in _unitOfWork.getStudentRepository.GetAll()
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
                studentList = studentList.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            else
            {
                searchString = currentFilter;
            }
           
            switch (sortOrder)
            {
                case "name_desc":
                    studentList = studentList.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentList = studentList.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentList = studentList.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentList = studentList.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Student>.CreateAsync((IQueryable<Student>)studentList, pageNumber ?? 1, pageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {            
            if (id == null)
            {
                return NotFound();
            }
            /*var student = await _unitOfWork.getStudentRepository.GetById(id??0);
            if (id == null || student == null)
            {
                return NotFound();
            }*/
            var student = await _studentReopsitory.GetStudentIncludeEnrollmentsIncludeCourse(id??0);
            
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//post
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.getStudentRepository.Insert(student);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }               
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _unitOfWork.getStudentRepository.GetById(id??0);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//Post
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            /*if (id == null)
            {
                return NotFound();
            }
            Student student =await _unitOfWork.getStudentRepository.GetById(id);

            if (await TryUpdateModelAsync<Student>(
                student,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)) {
                try
                {
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException *//* ex *//*)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.getStudentRepository.Update(student);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _unitOfWork.getStudentRepository.GetById(id ?? 0);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpGet, ActionName("Delete")]//Post
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _unitOfWork.getStudentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            
            _unitOfWork.getStudentRepository.Delete(student);      

            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
