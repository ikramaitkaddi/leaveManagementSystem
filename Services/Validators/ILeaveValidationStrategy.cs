using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services.Validators
{
    public interface ILeaveValidationStrategy
    {
        Task<bool> IsValid(LeaveRequest leaveRequest);
    }
}
