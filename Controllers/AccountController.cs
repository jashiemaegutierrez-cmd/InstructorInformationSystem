using Microsoft.AspNetCore.Mvc;
using InstructorInformationsSystem.Data;
using InstructorInformationsSystem.Models;

namespace InstructorInformationsSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Admins.Any(a => a.Email.ToLower() == model.Email.ToLower()))
                {
                    TempData["ErrorMessage"] = "An account with this email already exists.";
                    return View(model);
                }

                var newAdmin = new UserAccount
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };

                _context.Admins.Add(newAdmin);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = "Please fix the errors in the registration form.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = _context.Admins.FirstOrDefault(a =>
                    a.Email.ToLower() == model.Email.ToLower() && a.Password == model.Password);

                if (admin != null)
                {
                    HttpContext.Session.SetString("AdminUser", admin.Email);
                    HttpContext.Session.SetString("AdminName", $"{admin.FirstName} {admin.LastName}");

                    return RedirectToAction("Dashboard");
                }

                TempData["ErrorMessage"] = "Invalid login credentials. Please try again.";
                return View(model);
            }

            TempData["ErrorMessage"] = "Invalid email or password format.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUser")))
            {
                TempData["ErrorMessage"] = "You must log in to access the Dashboard.";
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }
    }
}