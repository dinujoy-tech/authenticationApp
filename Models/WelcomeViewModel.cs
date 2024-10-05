namespace authApp.Models
{
    public class WelcomeViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public List<EmployeeTask> Tasks { get; set; }
        public List<User> Employees { get; set; }
    }

}
