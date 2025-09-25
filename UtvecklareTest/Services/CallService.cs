using Newtonsoft.Json;
using UtvecklareTest.Models;
using UtvecklareTest.Services.Interfaces;

namespace UtvecklareTest.Services
{
    public class CallService : ICallService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _dataPath;

        public CallService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _dataPath = Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "calls.json");
        }

        public List<Call> GetAllCalls()
        {
            try
            {
                using (StreamReader r = new StreamReader(_dataPath))
                {
                    string json = r.ReadToEnd();
                    List<Call> calls = JsonConvert.DeserializeObject<List<Call>>(json) ?? new();
                    return calls;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading call data: {ex.Message}", ex);
            }
        }

        public DashboardViewModel GetDashboardData()
        {
            var calls = GetAllCalls();
            var employeeStats = GetEmployeeStatistics(calls);
            var dates = GetUniqueDates(calls);
            var employees = GetEmployeeNames(employeeStats);

            return new DashboardViewModel
            {
                Calls = calls,
                EmployeeStats = employeeStats,
                Employees = employees,
                Dates = dates
            };
        }

        public List<EmployeeStats> GetEmployeeStatistics(List<Call> calls)
        {
            return calls
                .SelectMany(c => c.Employees.Select(e => new { Date = c.date, Employee = e.Name, Calls = e.Calls }))
                .GroupBy(x => x.Employee)
                .Select(g => new EmployeeStats
                {
                    Name = g.Key,
                    TotalCalls = g.Sum(x => x.Calls),
                    DailyCalls = g.OrderBy(x => x.Date)
                                  .Select(x => new DailyCall { Date = x.Date.ToString("yyyy-MM-dd"), Calls = x.Calls })
                                  .ToList()
                }).ToList();
        }

        public List<string> GetUniqueDates(List<Call> calls)
        {
            return calls
                .Select(c => c.date.ToString("yyyy-MM-dd"))
                .Distinct()
                .OrderBy(d => d)
                .ToList();
        }

        public List<string> GetEmployeeNames(List<EmployeeStats> employeeStats)
        {
            return employeeStats
                .Select(e => e.Name)
                .ToList();
        }
    }
}