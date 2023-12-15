using System.Linq.Expressions;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        IQueryable<Course> Search(Expression<Func<Course, bool>>? predicate = null);
    }
}
