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
        public Task UpdateAsync(LeaveRequest request) => Task.FromResult(_context.LeaveRequests.Update(request));
        public Task DeleteAsync(LeaveRequest request) => Task.FromResult(_context.LeaveRequests.Remove(request));
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<LeaveRequest>> FilterLeaveRequests(int? employeeId, LeaveType? leaveType,
         LeaveStatus? status, DateTime? startDate, DateTime? endDate, string? keyword, int page, int pageSize,
         string sortBy, string sortOrder)
        {
            var query = _context.LeaveRequests.AsQueryable();

            if (employeeId.HasValue)
                query = query.Where(lr => lr.EmployeeId == employeeId.Value);

            if (leaveType.HasValue)
                query = query.Where(lr => lr.LeaveType == leaveType.Value);

            if (status.HasValue)
                query = query.Where(lr => lr.Status == status.Value);

            if (startDate.HasValue)
                query = query.Where(lr => lr.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(lr => lr.EndDate <= endDate.Value);

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(lr => lr.Reason.Contains(keyword));

            // Sorting
            if (sortOrder.ToLower() == "asc")
                query = query.OrderBy(lr => EF.Property<object>(lr, sortBy));
            else
                query = query.OrderByDescending(lr => EF.Property<object>(lr, sortBy));

            // Pagination
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task AddAsync(LeaveRequest leaveRequest)
        {
            _context.Entry(leaveRequest.Employee).State = EntityState.Unchanged;
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
        }
    }
}
