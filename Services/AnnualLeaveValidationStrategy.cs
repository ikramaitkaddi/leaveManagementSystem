using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services
{
    public class AnnualLeaveValidationStrategy : ILeaveValidationStrategy
    {
        private readonly ApplicationDbContext _context;

        public AnnualLeaveValidationStrategy(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsValid(LeaveRequest leaveRequest)
        {
            // Max 20 days annual leave per year logic
            var currentYear = leaveRequest.StartDate.Year;

            var annualLeaves = await _context.LeaveRequests
                .Where(l => l.EmployeeId == leaveRequest.EmployeeId &&
                            l.LeaveType == LeaveType.Annual &&
                            l.StartDate.Year == currentYear)
                .ToListAsync(); 

            int totalDaysTaken = annualLeaves.Sum(l => (l.EndDate - l.StartDate).Days + 1);

            int requestedDays = (leaveRequest.EndDate - leaveRequest.StartDate).Days + 1;

            return (totalDaysTaken + requestedDays) <= 20;
        }
    }

}
