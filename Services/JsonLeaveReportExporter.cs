namespace LeaveManagementSystem.Services
{
    using LeaveManagementSystem.DTOs;
    using LeaveManagementSystem.Interfaces;
    using System.Text.Json;

    public class JsonLeaveReportExporter : ILeaveReportExporter
    {
        public string Export(IEnumerable<LeaveReportDto> reportData)
        {
            return JsonSerializer.Serialize(reportData, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
