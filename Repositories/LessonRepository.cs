using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(WebDbContext context) : base(context)
        {
        }
    }
}
