﻿using System.ComponentModel.DataAnnotations;
using WebApi.Constant;

namespace WebApi.Dtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public GradleType Gradle { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
