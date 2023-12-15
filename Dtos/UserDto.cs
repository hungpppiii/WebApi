using System.ComponentModel.DataAnnotations;
using WebApi.Constant;

namespace WebApi.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public UserRole Role { get; set; }
        [MaxLength(100)]
        public string? Password { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
    }
}
