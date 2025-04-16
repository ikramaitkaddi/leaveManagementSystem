using LeaveManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LeaveManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
           
        {
              base.OnModelCreating(modelBuilder);
            // Seeding some dummy data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FullName = "Ikram Ait Kaddi", Department = "IT", JoiningDate = new DateTime(2021, 5, 12) },
                new Employee { Id = 2, FullName = "John Doe", Department = "RH", JoiningDate = new DateTime(2021, 5, 12) }
            );

            modelBuilder.Entity<LeaveRequest>().HasData(
                new LeaveRequest
                {
                    Id = 1,
                    EmployeeId = 1,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 5, 1).Date,
                    EndDate = new DateTime(2025, 5, 10).Date,
                    Status = LeaveStatus.Pending,
                    Reason = "Vacation"
                }
            );
        }
    }
}
