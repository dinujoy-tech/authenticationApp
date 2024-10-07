using System;
using System.ComponentModel.DataAnnotations;

namespace authApp.Models
{
    public class EmployeeTask
    {
        [Key]
        public int TaskId { get; set; }  // Primary Key

        [Required]
        public int UserId { get; set; }  // Foreign Key referencing Users(UserId)

        [Required(ErrorMessage = "Task Name is required.")]
        public string TaskName { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskStatus { get; set; } = "Pending";  // Default: Pending

        [Required]
        public DateTime AssignedDate { get; set; } = DateTime.Now;  // Default: current date

        [Required(ErrorMessage = "Deadline Date is required.")]
        public DateTime DeadlineDate { get; set; }

        public string Resources { get; set; }

        [Required(ErrorMessage = "Please select an employee.")]
        public int AssignedToUserId { get; set; }  // Changed to int
    }
}
