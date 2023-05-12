using ContosoUniversity.Data;
using ContosoUniversity.GenericRepository;
using ContosoUniversity.IUnitOfWork;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ContosoUniversity.UnitOfWork
{
    public class CUnitOfWork : InterfaceUnitOfWork
    {
        private SchoolContext _context;
        private GenericRepository<Course>? _courseRepository;
        private GenericRepository<Enrollment>? _enrollmentRepository;
        private GenericRepository<Student>? _studentRepository;
        private GenericRepository<Department>? _departmentRepository;
        private GenericRepository<Instructor>? _instructorRepository;
        private GenericRepository<MyTempTable>? _TempTableRepository;
        private TempTableHandler _TempTableHandler;
        public CUnitOfWork(SchoolContext context) { _context = context; _TempTableHandler = _context.GetTempTableHandler(); }  

        

        public GenericRepository<Course> getCourseRepository {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new GenericRepository<Course>(_context);
                }
                return _courseRepository;
            }
        }
        public GenericRepository<Instructor> getInstructorRepository
        {
            get
            {
                if (_instructorRepository == null)
                {
                    _instructorRepository = new GenericRepository<Instructor>(_context);
                }
                return _instructorRepository;
            }
        }

        public GenericRepository<MyTempTable> getTempTableRepository
        {
            get
            {
                if (_TempTableRepository == null)
                {
                    _TempTableRepository = new GenericRepository<MyTempTable>(_context);
                }
                return _TempTableRepository;
            }
        }
        public GenericRepository<Enrollment> getEnrollmentRepository
        {
            get
            {
                if (_enrollmentRepository == null)
                {
                    _enrollmentRepository = new GenericRepository<Enrollment>(_context);
                }
                return _enrollmentRepository;
            }
        }
        public GenericRepository<Student> getStudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new GenericRepository<Student>(_context);
                }
                return _studentRepository;
            }
        }

        public GenericRepository<Department> getDepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new GenericRepository<Department>(_context);
                }
                return _departmentRepository;
            }
        }

        public async Task CreateMyTempTableAsync(bool createPk,CancellationToken cancellationToken = default)
        {
            await _TempTableHandler.CreateMyTempTableAsync(true);      
        }

        public async Task<bool> DeleteMyTempTableAsync()
        {

            return await _TempTableHandler.DeleteMyTempTableAsync();
        }

        public IQueryable<int> GetMyTempTable()
        {
            return _TempTableHandler.getMyTempTable;
        }

        public async Task InsertMyTempTableAsync(IEnumerable<int> data)
        {
            await _TempTableHandler.BulkInsertIntoMyTempTableAsync(data);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        

    }
}
