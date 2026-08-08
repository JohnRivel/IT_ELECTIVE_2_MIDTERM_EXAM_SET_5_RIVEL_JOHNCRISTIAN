using System.Security.Claims;
using EquipmentBorrowingMonitoringSystem.Models;
using EquipmentBorrowingMonitoringSystem.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentBorrowingMonitoringSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userRepository.GetByUsername(username);

            if (user == null || user.Password != password)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

            var identity = new ClaimsIdentity(
                claims,
                "MyCookieAuth");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                "MyCookieAuth",
                principal);

            return RedirectToAction(
                "Index",
                "EquipmentBorrowing");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (_userRepository.GetByUsername(user.Username) != null)
            {
                ModelState.AddModelError(
                    "Username",
                    "Username already exists.");

                return View(user);
            }

            _userRepository.Add(user);

            TempData["Success"] =
                "Registration successful. You can now log in.";

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");

            return RedirectToAction("Login");
        }
    }
}