using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(WebDbContext context) : base(context)
        {
        }
    }
}
