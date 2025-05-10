using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace NTN.Controllers
{
    public class AccountController : Controller
    {
        private readonly CustomerContext _context;

        public AccountController(CustomerContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user, string password)
        {
            if (ModelState.IsValid)
            {
                user.PasswordHash = SimpleHash(password); // Basic hashing
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: /Account/Login
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && SimpleHash(password) == user.PasswordHash)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid credentials");
            return View();
        }

        private string SimpleHash(string input)
        {
            // WARNING: This is a DEMO-ONLY implementation
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}