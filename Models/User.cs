﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Role { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public ICollection<Course> EnrolledCourses { get; set; } = new HashSet<Course>();
        public ICollection<Course> TaughtCourses { get; set; } = new HashSet<Course>();
    }
}
