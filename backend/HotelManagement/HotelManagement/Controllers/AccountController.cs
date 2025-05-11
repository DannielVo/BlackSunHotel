using HotelManagement.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HotelManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DbAccount _context;

        public AccountController(DbAccount context)
        {
            _context = context;
        }

        // Đăng ký
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationRequest request)
        {
            // Kiểm tra email tồn tại
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("Email đã tồn tại");
            }

            var user = new User
            {
                Fullname = request.Fullname,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password // Lưu mật khẩu dạng plain text (KHÔNG AN TOÀN)
            };

            _context.Users.Add(user);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                Console.WriteLine(innerException.Message); // Log hoặc breakpoint để xem lỗi
            }

            return Ok("Đăng ký thành công");
        }

        // Đăng nhập
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Thông tin đăng nhập không chính xác");
            }

            // Lưu session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            return Ok("Đăng nhập thành công");
        }

        // Đăng xuất
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return Ok("Đã đăng xuất");
        }
    }

    public class UserRegistrationRequest
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
