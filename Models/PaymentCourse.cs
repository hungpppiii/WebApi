using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class PaymentCourse
    {
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
