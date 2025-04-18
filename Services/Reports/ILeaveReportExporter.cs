using LeaveManagementSystem.DTOs;

namespace LeaveManagementSystem.Services.Reports
{
    public interface ILeaveReportExporter
    {
        string Export(IEnumerable<LeaveReportDto> reportData);
    }
}
