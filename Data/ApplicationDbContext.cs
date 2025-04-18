using LeaveManagementSystem.Models;
using LeaveManagementSystem.Utilities.Seeders;
using Microsoft.EntityFrameworkCore;

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
           
        }
    }
}
