

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
                    HttpContext.Session.SetString("UserId", existingUser.UserId.ToString());

                    // Retrieve the user's role
                    HttpContext.Session.SetString("Role", existingUser.Role);
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
            // Fetch the current user from the session
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                // If the session doesn't contain a UserId, redirect to the login page
                return RedirectToAction("Login");
            }

            // Convert the session-stored UserId (string) to an integer
            int userId = int.Parse(userIdString);

            // Find the user in the database
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                // If the user is not found, redirect to the login page
                return RedirectToAction("Login");
            }

            // Pass the user details to the view
            return View(user);  // Assuming your view will use the User model or a view model with role information
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        // Additional actions (Logout, etc.) can be added here.
    }
}
