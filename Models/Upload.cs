using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authApp.Models
{
    public class Upload
    {
        [Key]
        public int UploadId { get; set; }  // Primary Key

        [Required]
        public int TaskId { get; set; }  // Foreign Key referencing EmployeeTasks(TaskId)

        [Required]
        public int UserId { get; set; }  // Foreign Key referencing Users(UserId)

        [Required]
        public string FilePath { get; set; }  // Path where the file is stored

        [Required]
        public DateTime UploadedAt { get; set; }  // When the file was uploaded

  
    }
}
