using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApi.Constant;

namespace WebApi.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Course : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public GradleType Gradle { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public Guid TeacherId { get; set; }
        public virtual User Teacher { get; set; }
        public virtual ICollection<User> Students { get; set; } = new HashSet<User>();
        public virtual ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
