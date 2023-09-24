using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Models
{
    public class Lesson : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public Course Course { get; set; }
    }
}
