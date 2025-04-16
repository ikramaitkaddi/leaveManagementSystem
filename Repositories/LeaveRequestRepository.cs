using LeaveManagementSystem.Data;
using LeaveManagementSystem.Interfaces;
using LeaveManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllAsync() => await _context.LeaveRequests.Include(l => l.Employee).ToListAsync();
        public async Task<LeaveRequest> GetByIdAsync(int id) => await _context.LeaveRequests.Include(l => l.Employee).FirstOrDefaultAsync(l => l.Id == id);
        public async Task AddAsync(LeaveRequest request) => await _context.LeaveRequests.AddAsync(request);
        public Task UpdateAsync(LeaveRequest request) => Task.FromResult(_context.LeaveRequests.Update(request));
        public Task DeleteAsync(LeaveRequest request) => Task.FromResult(_context.LeaveRequests.Remove(request));
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
