using Microsoft.AspNetCore.Mvc;
using authApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace authApp.Controllers
{
    public class EmpTaskListController : Controller
    {
        private readonly AppDbContext _context;

        public EmpTaskListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Assign Task to Employees (Only Manager role can access)
        [HttpGet]
        public async Task<IActionResult> AssignTask()
        {
            // Retrieve the user role from session
            var userRole = HttpContext.Session.GetString("Role");

            // Check if the role is "Manager"
            if (userRole == "Manager")
            {
                var model = new AssignTaskViewModel
                {
                    EmployeeTask = new EmployeeTask(),
                    Employees = await _context.Users.Where(u => u.Role == "Employee").ToListAsync()
                };
                return View(model);
            }

            // Redirect if not authorized
            return RedirectToAction("AccessDenied", "Account");
        }

        // Handle POST request for assigning tasks
        [HttpPost]
        public async Task<IActionResult> AssignTask(AssignTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add the EmployeeTask to the EmployeeTasks table
                _context.EmployeeTasks.Add(model.EmployeeTask);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect to a page to view tasks, or display a success message
                //return RedirectToAction("EmployeeTasks");
            }

            // Reload the list of employees in case of an error
            model.Employees = await _context.Users.Where(u => u.Role == "Employee").ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeTasks()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            // Ensure userIdString is not null
            if (userIdString != null)
            {
                var tasks = await _context.EmployeeTasks
                    .Where(t => t.AssignedToUserId == userIdString) // Ensure correct userId is checked
                    .ToListAsync();
                return View(tasks);
            }

            return RedirectToAction("AccessDenied", "Account"); // Redirect if session is missing or unauthorized
        }
    }
}
