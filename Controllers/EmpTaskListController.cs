using Microsoft.AspNetCore.Mvc;
using authApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace authApp.Controllers
{
    public class EmpTaskListController : Controller
    {
        private readonly AppDbContext _context;

        public EmpTaskListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: View for assigning tasks (only for managers)
        public IActionResult AssignTask()
        {
            // Fetch list of employees to assign the task to
            var employees = _context.Users.Where(u => u.Role == "Employee").ToList();
            ViewBag.Employees = employees;
            return View();
        }

        [HttpPost]
        
        public IActionResult AssignTask(EmployeeTask task)
        {
            if (ModelState.IsValid)
            {
                // Ensure the DueDate is in UTC
                task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);

                // Add the task to the database
                _context.EmployeeTasks.Add(task);
                _context.SaveChanges();

                // Clear the form by returning a new instance of EmployeeTask
                ModelState.Clear(); // This clears the form state, ensuring no old data is shown

                // Repopulate the employee list to show it in the dropdown
                var employeeList = _context.Users.Where(u => u.Role == "Employee").ToList();
                ViewBag.Employees = employeeList;

                return View(new EmployeeTask()); // Return a new instance of EmployeeTask
            }

            // If validation failed, repopulate the employee list
            var employeeListOnError = _context.Users.Where(u => u.Role == "Employee").ToList();
            ViewBag.Employees = employeeListOnError;

            return View(task); // Return the form with validation errors and filled data
        }






        // GET: View employee tasks (only for employees)
        public IActionResult TaskList()
        {
            // Retrieve the current user's UserId from the session
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if the session has expired or the user is not logged in
            }

            // Query the tasks assigned to the current user
            var tasks = _context.EmployeeTasks
                .Where(t => t.AssignedToUserId == currentUserId)
                .ToList();

            // Pass the list of tasks to the view
            return View(tasks);
        }

    }
}
