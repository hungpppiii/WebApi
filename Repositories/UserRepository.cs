using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WebDbContext context) : base(context)
        {
        }

        public async Task AddEnrolledCourseAsync(User student, Course course)
        {
            student.EnrolledCourses.Add(course);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddTaughtCourseAsync(User teacher, Course course)
        {
            teacher.TaughtCourses.Add(course);
            await _context.SaveChangesAsync();
        }
    }
}
