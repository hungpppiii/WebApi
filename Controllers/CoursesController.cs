using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Request;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return Ok(await _courseRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, CourseDto request)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            course.Name = request.Name;
            course.Price = request.Price;
            course.Gradle = request.Gradle;
            course.Description = request.Description ?? course.Description;
            course.TeacherId = request.TeacherId;

            if (course == null)
            {
                return NotFound();
            }

            try
            {
                await _courseRepository.UpdateAsync(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto request)
        {
            var course = new Course
            {
                Name = request.Name,
                Gradle = request.Gradle,
                Price = request.Price,
                Description = request.Description,
                TeacherId = request.TeacherId,
            };

            await _courseRepository.AddAsync(course);

            var courseDto = new CourseDto
            {
                Name = course.Name,
                Gradle = course.Gradle,
                Price = course.Price,
                Description = course.Description,
                TeacherId = course.TeacherId
            };

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, courseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            await _courseRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
