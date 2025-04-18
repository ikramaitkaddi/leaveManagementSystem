using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services
{
    public interface ILeaveRequestService
    {
        Task<(bool Success, string? Message, LeaveRequest? Leave)> ApproveLeaveRequestAsync(int id);
        Task<string> GenerateLeaveReport(int year, string? department, DateTime? from, DateTime? to, string format);
        Task<bool> IsLeaveValid(LeaveRequest leaveRequest);
    }
}
