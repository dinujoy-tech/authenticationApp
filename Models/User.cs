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
using System.ComponentModel.DataAnnotations.Schema;

namespace authApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(150)]
        public string ManagerName { get; set; }
        public int? ManagerId { get; set; }

        public DateTime DateOfJoining { get; set; }
    }
}

