namespace UtvecklareTest.Models
{
    public class EmployeeStats
    {
        public string Name { get; set; } = "";
        public int TotalCalls { get; set; }
        public List<DailyCall> DailyCalls { get; set; } = new();
    }
}