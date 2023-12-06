using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(WebDbContext context) : base(context)
        {
        }
    }
}
