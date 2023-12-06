using System.ComponentModel.DataAnnotations;
using WebApi.Constant;

namespace WebApi.Request
{
    public class CreateCourseRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public GradleType Gradle { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public string? Description { get; set; }
        public Guid TeacherId { get; set; }
    }
}
