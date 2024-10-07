using Microsoft.AspNetCore.Mvc;
using authApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

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
                // Ensure AssignedDate and DeadlineDate are set to UTC
                task.AssignedDate = DateTime.UtcNow;
                task.DeadlineDate = DateTime.SpecifyKind(task.DeadlineDate, DateTimeKind.Utc);

                // Add the task to the database
                _context.EmployeeTasks.Add(task);
                _context.SaveChanges();

                // Clear the form and repopulate the employee list
                ModelState.Clear();
                var employeeList = _context.Users.Where(u => u.Role == "Employee").ToList();
                ViewBag.Employees = employeeList;

                return View(new EmployeeTask()); // Clear form on success
            }

            // If validation failed, repopulate the employee list
            var employeeListOnError = _context.Users.Where(u => u.Role == "Employee").ToList();
            ViewBag.Employees = employeeListOnError;

            return View(task); // Return the form with validation errors and filled data
        }


        public IActionResult TaskList()
        {
            // Retrieve the current user's UserId from the session
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if the session has expired or the user is not logged in
            }

            // Convert UserId from string to int
            if (!int.TryParse(currentUserId, out int parsedUserId))
            {
                return RedirectToAction("Login", "Account"); // Invalid UserId
            }

            // Query the tasks assigned to the current user
            var tasks = _context.EmployeeTasks
                .Where(t => t.AssignedToUserId == parsedUserId) // Use parsedUserId instead
                .ToList();

            // Pass the list of tasks to the view
            return View(tasks);
        }

        // GET: Display task list and upload form
        [HttpGet]
        public IActionResult UploadTask()
        {
            // Retrieve the current user's UserId from the session
            var currentUserId = HttpContext.Session.GetString("UserId");

            // If the user is not logged in, redirect to login
            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if the session is expired
            }

            // Attempt to parse UserId (from session) to an integer
            if (!int.TryParse(currentUserId, out int userIdInt))
            {
                return RedirectToAction("Login", "Account"); // If UserId is not a valid integer, redirect to login
            }

            // Fetch tasks assigned to the current user
            var tasks = _context.EmployeeTasks
                .Where(t => t.AssignedToUserId == userIdInt)
                .ToList();

            // Create the view model
            var model = new UploadTaskViewModel
            {
                AssignedTasks = tasks,
                TaskId = 0, // Default to no specific task selected
                FilePath = string.Empty // Initialize empty string for file path
            };

            // Pass the model to the view
            return View(model);
        }

        // POST: Handle the task file upload and update task status
        [HttpPost]
        public async Task<IActionResult> UploadTask(UploadTaskViewModel model)
        {
            // Retrieve the current user's UserId from the session
            var currentUserId = HttpContext.Session.GetString("UserId");

            // If the user is not logged in, redirect to login
            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Attempt to parse UserId (from session) to an integer
            if (!int.TryParse(currentUserId, out int userIdInt))
            {
                return RedirectToAction("Login", "Account"); // If UserId is not a valid integer, redirect to login
            }

            // Attempt to parse TaskId (from model) to an integer
            if (!int.TryParse(model.TaskId.ToString(), out int taskId))
            {
                ModelState.AddModelError("TaskId", "Invalid Task ID.");
            }

            // If the model state is valid, proceed with file upload
            if (ModelState.IsValid)
            {
                // Create a new Upload instance with the manually entered file path
                var upload = new Upload
                {
                    TaskId = taskId,
                    UserId = userIdInt,
                    FilePath = model.FilePath, // Manually entered file path
                    UploadedAt = DateTime.UtcNow
                };

                // Add the upload record to the database
                _context.Uploads.Add(upload);
                await _context.SaveChangesAsync();

                // Check if the uploaded file is past the task's deadline
                var task = await _context.EmployeeTasks.FindAsync(taskId);
                if (task != null)
                {
                    // Update task status based on whether the file was uploaded before or after the deadline
                    if (upload.UploadedAt > task.DeadlineDate)
                    {
                        task.TaskStatus = "Overdue";  // Mark as overdue if past deadline
                    }
                    else
                    {
                        task.TaskStatus = "Submitted";  // Mark as submitted if before deadline
                    }

                    // Update the task status in the database
                    _context.EmployeeTasks.Update(task);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("TaskList"); // Redirect to task list after successful upload
            }

            // Re-fetch the tasks if the model state is not valid
            var tasks = _context.EmployeeTasks
                .Where(t => t.AssignedToUserId == userIdInt)
                .ToList();

            model.AssignedTasks = tasks; // Reassign the tasks to the model

            // If model is not valid, return the view with validation errors
            return View(model);
        }

        

    }
}
