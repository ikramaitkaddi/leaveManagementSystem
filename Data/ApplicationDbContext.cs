using LeaveManagementSystem.Models;
using LeaveManagementSystem.Seeders;
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
              CustomSeeder.Seed(modelBuilder);
            /* Seeding some dummy data
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
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FullName = "Alice Johnson",
                    Department = "HR",
                    JoiningDate = new DateTime(2022, 5, 10)
                },
                new Employee
                {
                    Id = 2,
                    FullName = "Bob Smith",
                    Department = "IT",
                    JoiningDate = new DateTime(2021, 11, 1)
                }
            );

            modelBuilder.Entity<LeaveRequest>().HasData(
                new LeaveRequest
                {
                    Id = 1,
                    EmployeeId = 1,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2024, 6, 10),
                    EndDate = new DateTime(2024, 6, 15),
                    Status = LeaveStatus.Pending,
                    Reason = "Vacation",
                    CreatedAt = new DateTime(2024, 5, 1)
                },
                new LeaveRequest
                {
                    Id = 2,
                    EmployeeId = 2,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2024, 7, 1),
                    EndDate = new DateTime(2024, 7, 3),
                    Status = LeaveStatus.Approved,
                    Reason = "Flu",
                    CreatedAt = new DateTime(2024, 6, 25)
                }
            );*/
        }
    }
}
