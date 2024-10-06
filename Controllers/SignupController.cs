
//using Microsoft.AspNetCore.Mvc;
//using authApp.Models;

//namespace authApp.Controllers
//{
//    public class SignupController : Controller
//    {
//        private readonly AppDbContext _context;

//        public SignupController(AppDbContext context)
//        {
//            _context = context;
//        }

//        //[HttpGet]
//        //public IActionResult ManagerSignup() => View();

//        [HttpPost]
//        public async Task<IActionResult> ManagerSignup(User user)
//        {
//            if (ModelState.IsValid)
//            {
//                user.Role = "Manager";
//                _context.Users.Add(user);
//                await _context.SaveChangesAsync();
//                return RedirectToAction("Login", "Account");
//            }
//            return View(user);
//        }

//        //[HttpGet]
//        //public IActionResult EmployeeSignup() => View();

//        [HttpPost]
//        public async Task<IActionResult> EmployeeSignup(User user)
//        {
//            if (ModelState.IsValid) 
//            {
//                user.Role = "Employee";
//                _context.Users.Add(user);
//                await _context.SaveChangesAsync();
//                return RedirectToAction("Login", "Account");
//            }
//            return View(user);
//        }
//    }
//}

using authApp.Models;
using Microsoft.AspNetCore.Mvc;


public class SignupController : Controller
{
    private readonly AppDbContext _context;

    public SignupController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Show signup form for Manager
    [HttpGet]
    public IActionResult ManagerSignup()
    {
        return View();
    }

    // POST: Handle signup for Manager
    [HttpPost]
    public async Task<IActionResult> ManagerSignup(User user)
    {
        if (ModelState.IsValid)
        {
            user.Role = "Manager";
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }
        return View(user);
    }

    // Similar methods for EmployeeSignup...
}
