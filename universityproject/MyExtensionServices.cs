
using ContosoUniversity.GenericRepository;
using ContosoUniversity.IGenericRepository;
using ContosoUniversity.IRepository;
using ContosoUniversity.IUnitOfWork;
using ContosoUniversity.Repository;
using ContosoUniversity.UnitOfWork;

namespace ContosoUniversity
{
    public static class MyExtensionServices
    {
        
        public static IServiceCollection getServices (IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<IStudentRepository,StudentRepository>();
            serviceProvider.AddScoped<ICourseRepository, CourseRepository>();
            serviceProvider.AddScoped<IInstructorRepository, InstructorRepository>();
            serviceProvider.AddScoped<InterfaceUnitOfWork,CUnitOfWork>();
            return serviceProvider;
        }
    }
}
