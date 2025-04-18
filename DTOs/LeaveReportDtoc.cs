namespace LeaveManagementSystem.DTOs
{
    public class LeaveReportDto
    {
        public string Employee { get; set; }
        public int TotalLeaves { get; set; }
        public int AnnualLeaves { get; set; }
        public int SickLeaves { get; set; }
    }
}
