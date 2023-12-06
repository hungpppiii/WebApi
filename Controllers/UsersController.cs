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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserDto request)
        {
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
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,
                Password = request!.Password,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };

            await _userRepository.AddAsync(user);

            var userDto = new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
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
