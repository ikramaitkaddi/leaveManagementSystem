namespace LeaveManagementSystem.Services.Reports
{
    using LeaveManagementSystem.DTOs;
    using System.Text.Json;

    public class JsonLeaveReportExporter : ILeaveReportExporter
    {
        public string Export(IEnumerable<LeaveReportDto> reportData)
        {
            return JsonSerializer.Serialize(reportData, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
