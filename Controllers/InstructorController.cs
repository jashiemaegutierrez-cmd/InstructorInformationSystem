using Microsoft.AspNetCore.Mvc;
using InstructorInformationsSystem.Data;
using InstructorInformationsSystem.Models;

namespace InstructorInformationsSystem.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdminLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUser"));
        }

        // GET: Instructor/Index
        public IActionResult Index()
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Please log in first to view or manage instructors.";
                return RedirectToAction("Login", "Account");
            }
            return View(_context.Instructors.ToList());
        }

        // GET: Instructor/Create
        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Please log in first to add an instructor.";
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Instructor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instructor instructor)
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            if (!string.IsNullOrWhiteSpace(instructor.InstructorId))
            {
                instructor.InstructorId = instructor.InstructorId.Trim();

                if (_context.Instructors.Any(i => i.InstructorId == instructor.InstructorId))
                {
                    ModelState.AddModelError("InstructorId", $"Instructor ID '{instructor.InstructorId}' is already taken.");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Instructors.Add(instructor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(instructor);
        }

        // GET: Instructor/Edit/24-000
        public IActionResult Edit(string id)
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Please log in first to edit instructors.";
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(id)) return NotFound();

            var instructor = _context.Instructors.Find(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        // POST: Instructor/Edit/24-000
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Instructor instructor)
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            if (id != instructor.InstructorId) return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Instructors.Update(instructor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructor/Delete/24-000
        public IActionResult Delete(string id)
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Please log in first to delete instructors.";
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(id)) return NotFound();

            var instructor = _context.Instructors.Find(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        // POST: Instructor/Delete/24-000
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            var instructor = _context.Instructors.Find(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Instructor/Details/24-000
        public IActionResult Details(string id)
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Please log in first to view instructor details.";
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(id)) return NotFound();

            var instructor = _context.Instructors.Find(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }
    }
}