using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace authApp.Models
{
    public class UploadTaskViewModel
    {
        public List<EmployeeTask> AssignedTasks { get; set; } // Holds the tasks assigned to the employee

        public int TaskId { get; set; } // TaskId to identify which task the employee is uploading for

        [Required(ErrorMessage = "File path is required.")]
        public string FilePath { get; set; } // For entering the file path manually
    }
}
