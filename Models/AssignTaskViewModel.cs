using System.Collections.Generic;

namespace authApp.Models
{
    public class AssignTaskViewModel
    {
        public EmployeeTask EmployeeTask { get; set; }
        public List<User> Employees { get; set; } // List of employees
    }
}

