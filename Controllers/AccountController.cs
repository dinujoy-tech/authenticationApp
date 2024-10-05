

using Microsoft.AspNetCore.Mvc;
using authApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace authApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

                if (existingUser != null)
                {
                    // Log the user ID being set in session
                    HttpContext.Session.SetString("UserId", existingUser.UserId.ToString());
                    return RedirectToAction("Welcome", "Account");
                }

                // Log invalid login attempt
                ModelState.AddModelError("", "Invalid username or password.");
            }
            else
            {
                // Log validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                // Here you can log the errors if you have a logging mechanism in place
            }

            return View(user);
        }


        public IActionResult Welcome()
        {
            // Get the current user ID from session
            var userId = HttpContext.Session.GetString("UserId");

            // Pass the user ID to the view using ViewBag
            ViewBag.UserId = userId;

            return View();
        }


        // Additional actions (Logout, etc.) can be added here.
    }
}
