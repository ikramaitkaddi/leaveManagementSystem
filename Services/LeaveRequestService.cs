using LeaveManagementSystem.Data;
using LeaveManagementSystem.DTOs;
using LeaveManagementSystem.Enums;
using LeaveManagementSystem.Models;
using LeaveManagementSystem.NewFolder;
using LeaveManagementSystem.Services.Reports;
using LeaveManagementSystem.Services.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services
{
   
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeaveValidationStrategyFactory _validationStrategyFactory;

        public LeaveRequestService(ApplicationDbContext context, ILeaveValidationStrategyFactory validationStrategyFactory)
        {
            _context = context;
            _validationStrategyFactory = validationStrategyFactory;
        }

        public async Task<bool> IsLeaveValid(LeaveRequest leaveRequest)
        {
            // Check for overlapping leave dates
            var overlappingLeaveRequests = await _context.LeaveRequests
                .Where(lr => lr.EmployeeId == leaveRequest.EmployeeId &&
                             ((lr.StartDate >= leaveRequest.StartDate && lr.StartDate <= leaveRequest.EndDate) ||
                              (lr.EndDate >= leaveRequest.StartDate && lr.EndDate <= leaveRequest.EndDate)))
                .ToListAsync();

            if (overlappingLeaveRequests.Any())
            {
                return false;
            }

            // Use of the strategy pattern to validate the leave request
            var strategy = _validationStrategyFactory.GetStrategy(leaveRequest.LeaveType);
            return await strategy.IsValid(leaveRequest);
        }

        public async Task<string> GenerateLeaveReport(int year, string? department, DateTime? from, DateTime? to, string format)
        {
            AppLogger.Instance.Log($"Generating report for year: {year}, format: {format}");
            var query = _context.LeaveRequests
                .Include(lr => lr.Employee)
                .Where(lr => lr.StartDate.Year == year);

            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(lr => lr.Employee.Department == department);
                AppLogger.Instance.Log($"Filtering by department: {department}");
            }
            if (from.HasValue && to.HasValue)
            {
                query = query.Where(lr => lr.StartDate >= from && lr.EndDate <= to);
                AppLogger.Instance.Log($"Filtering by date range: {from} to {to}");
            }

            var groupedData = (await query.ToListAsync())
                .GroupBy(lr => lr.Employee)
                .Select(g => new LeaveReportDto
                {
                    Employee = g.Key.FullName,
                    TotalLeaves = g.Sum(lr => (lr.EndDate - lr.StartDate).Days + 1),
                    AnnualLeaves = g.Where(lr => lr.LeaveType == LeaveType.Annual).Sum(lr => (lr.EndDate - lr.StartDate).Days + 1),
                    SickLeaves = g.Where(lr => lr.LeaveType == LeaveType.Sick).Sum(lr => (lr.EndDate - lr.StartDate).Days + 1)
                }).ToList();
            //use of Factory Pattern in the report endpoint to dynamically generate different report formats (JSON, CSV)
            var exporter = LeaveReportExporterFactory.GetExporter(format);

            // implement the Singleton Pattern for centralized logging
            AppLogger.Instance.Log($"Report generated successfully with {groupedData.Count} records.");

            return exporter.Export(groupedData);
        }
        public async Task<(bool Success, string? Message, LeaveRequest? Leave)> ApproveLeaveRequestAsync(int id)
        {
            var leave = await _context.LeaveRequests.FindAsync(id);
            if (leave == null)
                return (false, "Leave request not found.", null);

            if (leave.Status != LeaveStatus.Pending)
                return (false, "Only pending requests can be approved.", null);

            leave.Status = LeaveStatus.Approved;
            await _context.SaveChangesAsync();

            AppLogger.Instance.Log($"LeaveRequest {id} approved successfully.");

            return (true, null, leave);
        }
        
    }

}
