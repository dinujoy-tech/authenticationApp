using Microsoft.AspNetCore.Mvc;
using authApp.Models;
using Microsoft.EntityFrameworkCore;

namespace authApp.Controllers
{
    public class EmpTaskListController : Controller
    {
        private readonly AppDbContext _context;

        public EmpTaskListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Assign Task view (only accessible by managers)
        [HttpGet]
        public async Task<IActionResult> AssignTask()
        {
            // Ensure that the user is a manager
            if (User.IsInRole("Manager"))
            {
                var model = new AssignTaskViewModel
                {
                    EmployeeTask = new EmployeeTask(), // Initialize EmployeeTask
                    Employees = await _context.Users.Where(u => u.Role == "Employee").ToListAsync() // Get list of employees
                };
                return View(model);
            }
            return RedirectToAction("AccessDenied", "Home"); // Redirect if not authorized
        }

        // POST: Assign task
        [HttpPost]
        public async Task<IActionResult> AssignTask(AssignTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the task to the database
                _context.EmployeeTasks.Add(model.EmployeeTask);
                await _context.SaveChangesAsync();

                // Optionally, you could add a success message here
                ViewBag.SuccessMessage = "Task assigned successfully.";
            }
            else
            {
                // Re-populate the employees list if the model state is invalid
                model.Employees = await _context.Users
                    .Where(u => u.Role == "Employee")
                    .ToListAsync();
            }

            // Stay on the assignment page regardless of model state
            return View(model);
        }


        // GET: View assigned tasks for employees
        [HttpGet]
        public async Task<IActionResult> EmployeeTasks()
        {
            // Get the current user ID from session
            var userId = HttpContext.Session.GetString("UserId");
            // Fetch tasks assigned to the employee
            var tasks = await _context.EmployeeTasks
                .Where(t => t.AssignedToUserId == userId) // Filter by the employee's user ID
                .ToListAsync();

            return View(tasks);
        }

        // Other actions...
    }
}
