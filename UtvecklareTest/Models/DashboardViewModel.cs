namespace UtvecklareTest.Models
{
    public class DashboardViewModel
    {
        public List<Call> Calls { get; set; } = new();
        public List<EmployeeStats> EmployeeStats { get; set; } = new();
        public List<string> Employees { get; set; } = new();
        public List<string> Dates { get; set; } = new();
        
        public List<DashboardCard> DashboardCards { get; set; } = new();
    }

    public class DashboardCard
    {
        public string Value { get; set; } = "";
        public string Title { get; set; } = "";
        public string Icon { get; set; } = "";
        public string BackgroundClass { get; set; } = "";
    }
}
