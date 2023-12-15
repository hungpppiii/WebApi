using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Payment : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<PaymentCourse> PaymentCourses { get; set; } = new HashSet<PaymentCourse>();
    }
}
