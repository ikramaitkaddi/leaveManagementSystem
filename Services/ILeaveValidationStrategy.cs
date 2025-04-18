using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services
{
    public interface ILeaveValidationStrategy
    {
        Task<bool> IsValid(LeaveRequest leaveRequest);
    }
}
