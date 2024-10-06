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

        private DateTime dueDate;
        [Required(ErrorMessage = "Due Date is required.")]
        public DateTime DueDate
        {
            get => dueDate;
            set => dueDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); // Ensure all dates are stored as UTC.
        }

        [Required(ErrorMessage = "Please select an employee.")]
        public string AssignedToUserId { get; set; }
    }

}
