using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Constant;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] int pageNumber, int pageSize)
        {
            var results = _mapper.Map<List<CourseDto>>(await _userRepository.Search().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserDto>(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserDto request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var user = await _userRepository.GetByIdAsync(id);

            user.Name = request.Name;
            user.Email = request.Email;
            user.Role = request.Role;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.Address = request.Address ?? user.Address;

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto request)
        {
            var user = _mapper.Map<User>(request);

            await _userRepository.AddAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserDto>(user));
        }

        [HttpPost("{id}/taught-course")]
        public async Task<ActionResult<CourseDto>> AddTaughtCourse([FromRoute] Guid id, [FromBody] CourseDto course)
        {
            var existedUser = await _userRepository.GetByIdAsync(id);

            if (existedUser == null || existedUser.Role != UserRole.Teacher)
            {
                return BadRequest();
            }

            var newCourse = _mapper.Map<Course>(course);

            await _userRepository.AddTaughtCourseAsync(existedUser, newCourse);

            return CreatedAtAction(nameof(GetUser), new { id = newCourse.Id }, _mapper.Map<CourseDto>(newCourse));
        }

        [HttpPost("{id}/enrolled-course")]
        public async Task<ActionResult<CourseDto>> AddEnrolledCourse([FromRoute] Guid id, [FromBody] CourseDto course)
        {
            var existedUser = await _userRepository.GetByIdAsync(id);

            if (existedUser == null || existedUser.Role != UserRole.Student)
            {
                return BadRequest();
            }

            var newCourse = _mapper.Map<Course>(course);

            await _userRepository.AddEnrolledCourseAsync(existedUser, newCourse);

            return CreatedAtAction(nameof(GetUser), new { id = newCourse.Id }, _mapper.Map<CourseDto>(newCourse));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
