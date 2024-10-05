using Microsoft.EntityFrameworkCore;

namespace authApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // Ensure this is correctly defined
        public DbSet<EmployeeTask> EmployeeTasks { get; set; } // Ensure this line exists

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // User-Manager relationship
        //    modelBuilder.Entity<User>()
        //        .HasOne(u => u.Manager)
        //        .WithMany()
        //        .HasForeignKey(u => u.ManagerId);

        //    // Task Assignment relationships
        //    modelBuilder.Entity<EmployeeTask>()
        //        .HasOne(t => t.AssignedTo)
        //        .WithMany(u => u.Tasks)
        //        .HasForeignKey(t => t.AssignedToId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<EmployeeTask>()
        //        .HasOne(t => t.AssignedBy)
        //        .WithMany()
        //        .HasForeignKey(t => t.AssignedById)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
    }

}
