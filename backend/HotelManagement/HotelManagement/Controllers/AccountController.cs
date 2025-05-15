// AccountController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using HotelManagement.Models;
using HotelManagement.DTOs;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly HotelSQL _context;

        public AccountController(HotelSQL context)
        {
            _context = context;
        }

        // POST: api/Account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO newUser)
        {
            if (await _context.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                return BadRequest("Email already in use.");
            }

            var user = new User
            {
                Fullname = newUser.Fullname,
                Email = newUser.Email,
                Phone = newUser.Phone,
                Password = newUser.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.UserId, user.Fullname, user.Email });
        }

        // POST: api/Account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Validate the user credentials by matching email and password
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);
            if (user == null)
            {
                return BadRequest("Invalid email or password.");
            }

            // Return user details (excluding password)
            return Ok(new
            {
                user.UserId,
                user.Fullname,
                user.Email,
                user.Phone,
                user.IsStaff,
                user.RoleName
            });
        }

        // PUT: api/Account/change-password
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            // Find user by ID
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the old password matches
            if (user.Password != request.OldPassword)
            {
                return BadRequest("Old password is incorrect.");
            }

            // Update to the new password
            user.Password = request.NewPassword;
            await _context.SaveChangesAsync();

            return Ok("Password updated successfully.");
        }
    }

    // Request model for login (email and password)
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    // Request model for changing password
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
