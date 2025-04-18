using LeaveManagementSystem.DTOs;

namespace LeaveManagementSystem.Interfaces
{
    public interface ILeaveReportExporter
    {
        string Export(IEnumerable<LeaveReportDto> reportData);
    }
}
