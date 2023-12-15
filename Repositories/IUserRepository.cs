using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task AddEnrolledCourseAsync(User student, Course course);
        Task AddTaughtCourseAsync(User teacher, Course course);
    }
}
