using System.ComponentModel.DataAnnotations;

namespace authApp.Models
{
    public class EmployeeTask
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Task Name is required.")]
        public string TaskName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Please select an employee.")]
        public string AssignedToUserId { get; set; } // This property links the task to the assigned employee

        // Additional properties can be added as needed
    }
}
