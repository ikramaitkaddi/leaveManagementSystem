using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task<IEnumerable<LeaveRequest>> GetAllAsync();
        Task<LeaveRequest> GetByIdAsync(int id);
        Task AddAsync(LeaveRequest request);
        Task UpdateAsync(LeaveRequest request);
        Task DeleteAsync(LeaveRequest request);
        Task SaveChangesAsync();
    }
}
