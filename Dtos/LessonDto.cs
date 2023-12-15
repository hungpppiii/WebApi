using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
    }
}
