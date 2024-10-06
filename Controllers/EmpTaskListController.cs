using Microsoft.AspNetCore.Mvc;
using authApp.Models;
using System.Linq;

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

                _context.EmployeeTasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("TaskList");
            }
            return View(task);
        }


        // GET: View employee tasks (only for employees)
        public IActionResult TaskList()
        {
            var currentUser = User.Identity.Name; // Assuming the username is stored in the session
            var tasks = _context.EmployeeTasks
                .Where(t => t.AssignedToUserId == currentUser)
                .ToList();
            return View(tasks);
        }
    }
}
