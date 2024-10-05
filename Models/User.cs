//using System.ComponentModel.DataAnnotations;

//namespace authApp.Models
//{
//    public class User
//    {
//        public int UserId { get; set; }

//        [Required]
//        [MaxLength(100)]
//        public string Username { get; set; }

//        [Required]
//        [MaxLength(100)]
//        public string Password { get; set; }

//        [Required]
//        [MaxLength(100)]
//        public string Email { get; set; }

//        [Required]
//        public string FullName { get; set; }

//        [Required]
//        public string Role { get; set; } // Manager or Employee

//        public int? ManagerId { get; set; }
//        public User Manager { get; set; }

//        public DateTime DateOfJoining { get; set; }

//        // Navigation property for tasks
//        public ICollection<EmployeeTask> Tasks { get; set; }
//    }

//}



using System.ComponentModel.DataAnnotations;

namespace authApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } // New property to define user role

        // Additional properties (e.g., Email, FullName) can be added here.
        //public string Email { get; set; } // Optional Email property
        //public string FullName { get; set; } // Optional FullName property
    }
}

