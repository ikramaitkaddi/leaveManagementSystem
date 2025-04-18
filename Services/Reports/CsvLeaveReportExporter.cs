﻿namespace LeaveManagementSystem.Services.Reports
{
    using LeaveManagementSystem.DTOs;
    using System.Text;

    public class CsvLeaveReportExporter : ILeaveReportExporter
    {
        public string Export(IEnumerable<LeaveReportDto> reportData)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Employee,TotalLeaves,AnnualLeaves,SickLeaves");

            foreach (var r in reportData)
            {
                sb.AppendLine($"{r.Employee},{r.TotalLeaves},{r.AnnualLeaves},{r.SickLeaves}");
            }

            return sb.ToString();
        }
    }
}
