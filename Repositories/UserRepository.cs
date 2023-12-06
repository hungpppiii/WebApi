using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WebDbContext context) : base(context)
        {
        }
    }
}
