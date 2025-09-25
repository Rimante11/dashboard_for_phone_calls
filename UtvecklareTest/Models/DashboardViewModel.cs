namespace UtvecklareTest.Models
{
    public class DashboardViewModel
    {
        public List<Call> Calls { get; set; } = new();
        public List<EmployeeStats> EmployeeStats { get; set; } = new();
        public List<string> Employees { get; set; } = new();
        public List<string> Dates { get; set; } = new();
    }
}
