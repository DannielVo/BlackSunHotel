using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;
using HotelManagement.DTOs;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HotelSQL _context;

        public UsersController(HotelSQL context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsers()
        {
            return await _context.Users
                .Select(u => new UserResponseDTO
                {
                    UserId = u.UserId,
                    Fullname = u.Fullname,
                    Email = u.Email,
                    Phone = u.Phone,
                    IsStaff = u.IsStaff,
                    RoleName = u.RoleName
                })
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserResponseDTO
                {
                    UserId = u.UserId,
                    Fullname = u.Fullname,
                    Email = u.Email,
                    Phone = u.Phone,
                    IsStaff = u.IsStaff,
                    RoleName = u.RoleName
                })
                .FirstOrDefaultAsync();

            if (user == null) return NotFound();
            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> PostUser([FromBody] UsersRequestDTO dto)
        {
            var user = new User
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                Phone = dto.Phone,
                IsStaff = dto.IsStaff,
                RoleName = dto.RoleName,
                Password = dto.Password // Plaintext storage
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId },
                new UserResponseDTO
                {
                    UserId = user.UserId,
                    Fullname = user.Fullname,
                    Email = user.Email,
                    Phone = user.Phone,
                    IsStaff = user.IsStaff,
                    RoleName = user.RoleName
                });
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UsersRequestDTO dto)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return NotFound();

            existingUser.Fullname = dto.Fullname;
            existingUser.Email = dto.Email;
            existingUser.Phone = dto.Phone;
            existingUser.IsStaff = dto.IsStaff;
            existingUser.RoleName = dto.RoleName;

            // Update password if provided
            if (!string.IsNullOrEmpty(dto.Password))
            {
                existingUser.Password = dto.Password; // Plaintext update
            }

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
