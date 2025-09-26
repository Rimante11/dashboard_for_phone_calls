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
                string json = File.ReadAllText(_dataPath);
                return JsonConvert.DeserializeObject<List<Call>>(json) ?? new();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading call data: {ex.Message}", ex);
            }
        }

        public DashboardViewModel GetDashboardData()
        {
            try
            {
                var calls = GetAllCalls();
                var employeeStats = GetEmployeeStatistics(calls);
                var dates = GetUniqueDates(calls);
                var employees = GetEmployeeNames(employeeStats);
                var dashboardCards = GetDashboardCards(calls, employees, dates);

                return new DashboardViewModel
                {
                    Calls = calls,
                    EmployeeStats = employeeStats,
                    Employees = employees,
                    Dates = dates,
                    DashboardCards = dashboardCards
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error generating dashboard data: {ex.Message}", ex);
            }
        }

        public List<EmployeeStats> GetEmployeeStatistics(List<Call> calls)
        {
            if (calls == null || !calls.Any()) return new();

            return calls
                .Where(c => c.Employees != null && c.Employees.Any())
                .SelectMany(c => c.Employees
                    .Where(e => !string.IsNullOrEmpty(e.Name))
                    .Select(e => new { Date = c.date, Employee = e.Name, Calls = e.Calls }))
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
            if (calls == null || !calls.Any())
                return new List<string>();

            return calls
                .Select(c => c.date.ToString("yyyy-MM-dd"))
                .Distinct()
                .OrderBy(d => d)
                .ToList();
        }

        public List<string> GetEmployeeNames(List<EmployeeStats> employeeStats) =>
            employeeStats?.Select(e => e.Name).ToList() ?? new List<string>();

        public List<DashboardCard> GetDashboardCards(List<Call> calls, List<string> employees, List<string> dates)
        {
            var totalCalls = 0;
            var employeeCount = employees?.Count ?? 0;
            var dayCount = dates?.Count ?? 0;

            if (calls != null && calls.Any())
            {
                totalCalls = calls
                    .SelectMany(c => c.Employees ?? new List<Employee>())
                    .Sum(e => e.Calls);
            }

            return new List<DashboardCard>
            {
                new DashboardCard 
                { 
                    Value = totalCalls.ToString(), 
                    Title = "Totala Samtal", 
                    Icon = "fas fa-phone", 
                    BackgroundClass = "bg-primary" 
                },
                new DashboardCard 
                { 
                    Value = employeeCount.ToString(), 
                    Title = "Antal Anställda", 
                    Icon = "fas fa-users", 
                    BackgroundClass = "bg-secondary" 
                },
                new DashboardCard 
                { 
                    Value = dayCount.ToString(), 
                    Title = "Antal Dagar", 
                    Icon = "fas fa-calendar", 
                    BackgroundClass = "bg-success" 
                }
            };
        }

    }
}