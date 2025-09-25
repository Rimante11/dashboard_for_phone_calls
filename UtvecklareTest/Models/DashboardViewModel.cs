namespace UtvecklareTest.Models
{
    public class DashboardViewModel
    {
        public List<Call> Calls { get; set; } = new();
        public List<EmployeeStats> EmployeeStats { get; set; } = new();
        public List<string> Employees { get; set; } = new();
        public List<string> Dates { get; set; } = new();
    }

    public class EmployeeStats
    {
        public string Name { get; set; } = "";
        public int TotalCalls { get; set; }
        public List<DailyCall> DailyCalls { get; set; } = new();
    }

    public class DailyCall
    {
        public string Date { get; set; } = "";
        public int Calls { get; set; }
    }
}
