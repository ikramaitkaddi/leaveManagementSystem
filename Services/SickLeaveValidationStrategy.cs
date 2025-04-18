using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services
{
    public class SickLeaveValidationStrategy : ILeaveValidationStrategy
    {
        public Task<bool> IsValid(LeaveRequest leaveRequest)
        {
            // Sick leave requires a reason
            return Task.FromResult(!string.IsNullOrEmpty(leaveRequest.Reason));
        }
    }

}
