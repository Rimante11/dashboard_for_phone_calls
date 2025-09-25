using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UtvecklareTest.Models;

namespace UtvecklareTest.Controllers
{
    public class HomeController : Controller
    {
        string _root {  get; set; }
        public HomeController(IWebHostEnvironment webHost)
        {
            _root = webHost.ContentRootPath;
        }


        public IActionResult Index()
        {
            List<Call> calls = GetData();

            var employeeStats = calls
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

            var dates = calls.Select(c => c.date.ToString("yyyy-MM-dd")).Distinct().OrderBy(d => d).ToList();
            var employees = employeeStats.Select(e => e.Name).ToList();

            var model = new DashboardViewModel
            {
                Calls = calls,
                EmployeeStats = employeeStats,
                Employees = employees,
                Dates = dates
            };

            return View(model);
        }

        List<Call> GetData()
        {
            using (StreamReader r = new StreamReader($"{_root}/App_Data/calls.json"))
            {
                string json = r.ReadToEnd();
                List<Call> calls = JsonConvert.DeserializeObject<List<Call>>(json) ?? new();
                return calls;
            }
        }
    }
}
