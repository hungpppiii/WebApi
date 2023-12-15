using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly LessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public LessonsController(LessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons([FromQuery] int pageNumber, int pageSize)
        {
            var results = _mapper.Map<List<CourseDto>>(await _lessonRepository.Search().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLesson(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return _mapper.Map<LessonDto>(lesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(Guid id, LessonDto request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var lesson = await _lessonRepository.GetByIdAsync(id);

            lesson.Name = request.Name;
            lesson.Description = request.Description;

            if (lesson == null)
            {
                return NotFound();
            }

            try
            {
                await _lessonRepository.UpdateAsync(lesson);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> CreateLesson(LessonDto request)
        {
            var lesson = _mapper.Map<Lesson>(request);

            await _lessonRepository.AddAsync(lesson);

            return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, _mapper.Map<LessonDto>(lesson));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            await _lessonRepository.DeleteAsync(id);

            return NoContent();
        }

    }
}
