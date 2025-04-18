namespace LeaveManagementSystem.Services.Reports
{
    public static class LeaveReportExporterFactory
    {
        public static ILeaveReportExporter GetExporter(string type)
        {
            return type.ToLower() switch
            {
                "json" => new JsonLeaveReportExporter(),
                "csv" => new CsvLeaveReportExporter(),
                _ => throw new NotSupportedException("Unsupported report type")
            };
        }
    }
}
