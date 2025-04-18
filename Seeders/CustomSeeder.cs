using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Seeders
{
    public static class CustomSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 3,
                    FullName = "Alice Johnson",
                    Department = "HR",
                    JoiningDate = new DateTime(2022, 5, 10)
                },
                new Employee
                {
                    Id = 4,
                    FullName = "Bob Smith",
                    Department = "IT",
                    JoiningDate = new DateTime(2021, 11, 1)
                }
            );

            modelBuilder.Entity<LeaveRequest>().HasData(
                new LeaveRequest
                {
                    Id = 3,
                    EmployeeId = 2,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2024, 6, 10),
                    EndDate = new DateTime(2024, 6, 15),
                    Status = LeaveStatus.Pending,
                    Reason = "Vacation",
                    CreatedAt = new DateTime(2024, 5, 1)
                },
                new LeaveRequest
                {
                    Id = 4,
                    EmployeeId = 2,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2024, 7, 1),
                    EndDate = new DateTime(2024, 7, 3),
                    Status = LeaveStatus.Approved,
                    Reason = "Flu",
                    CreatedAt = new DateTime(2024, 6, 25)
                }
            );
        }
    }

}
