using System.Collections.Generic;
using authApp.Models;

namespace authApp.Models
{
    public class AssignTaskViewModel
    {
        public EmployeeTask EmployeeTask { get; set; }
        public List<User> Employees { get; set; } // Assuming User is the model for your employees
    }
}
