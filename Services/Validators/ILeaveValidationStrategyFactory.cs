using LeaveManagementSystem.Enums;

namespace LeaveManagementSystem.Services.Validators
{
    public interface ILeaveValidationStrategyFactory
    {
        ILeaveValidationStrategy GetStrategy(LeaveType leaveType);
    }
}
