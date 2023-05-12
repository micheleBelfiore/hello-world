using ContosoUniversity.Data;
using ContosoUniversity.GenericRepository;
using ContosoUniversity.Models;

namespace ContosoUniversity.IUnitOfWork
{
    public interface InterfaceUnitOfWork
    {
        GenericRepository<Course> getCourseRepository { get; }
        GenericRepository<Enrollment> getEnrollmentRepository { get; }
        GenericRepository<Student> getStudentRepository { get; }
        GenericRepository<Department> getDepartmentRepository { get; }
        GenericRepository<MyTempTable> getTempTableRepository { get; }
        GenericRepository<Instructor> getInstructorRepository { get; }
        Task CreateMyTempTableAsync(bool createPk,
        CancellationToken cancellationToken = default);

        IQueryable<int> GetMyTempTable();
        Task InsertMyTempTableAsync(IEnumerable<int> data);

        Task<bool> DeleteMyTempTableAsync();
        Task SaveAsync();
    }
}
