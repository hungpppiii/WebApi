using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses([FromQuery] int pageNumber, int pageSize)
        {
            var results = _mapper.Map<List<CourseDto>>(await _courseRepository.Search().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return _mapper.Map<CourseDto>(course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, CourseDto request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var course = await _courseRepository.GetByIdAsync(id);

            course.Name = request.Name;
            course.Price = request.Price;
            course.Gradle = request.Gradle;
            course.Description = request.Description ?? course.Description;

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
            var course = _mapper.Map<Course>(request);

            await _courseRepository.AddAsync(course);

            var result = _mapper.Map<CourseDto>(course);

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, result);
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
