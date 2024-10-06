using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace authApp.Models
{
    public class UploadTaskViewModel
    {
        public List<EmployeeTask> AssignedTasks { get; set; } // Holds the tasks assigned to the employee

        public int TaskId { get; set; } // TaskId to identify which task the employee is uploading for

        [Required]
        public IFormFile UploadedFile { get; set; } // For uploading the file
    }
}
