using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class UserDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Role { get; set; }
        [MaxLength(100)]
        public string? Password { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
    }
}
