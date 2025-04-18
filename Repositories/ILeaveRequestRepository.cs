using LeaveManagementSystem.Enums;
using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Repositories
{
    public interface ILeaveRequestRepository
    {
        Task<IEnumerable<LeaveRequest>> GetAllAsync();
        Task<LeaveRequest?> GetByIdAsync(int id);
        Task UpdateAsync(LeaveRequest request);
        Task DeleteAsync(LeaveRequest request);
        Task SaveChangesAsync();
        Task<IEnumerable<LeaveRequest>> FilterLeaveRequests(int? employeeId, LeaveType? leaveType, LeaveStatus? status,
        DateTime? startDate, DateTime? endDate, string keyword, int page, int pageSize, string sortBy, string sortOrder);
        Task AddAsync(LeaveRequest leaveRequest);
    }
}
