using UtvecklareTest.Models;

namespace UtvecklareTest.Services.Interfaces
{
    public interface ICallService
    {
        List<Call> GetAllCalls();
        DashboardViewModel GetDashboardData();
        List<EmployeeStats> GetEmployeeStatistics(List<Call> calls);
        List<string> GetUniqueDates(List<Call> calls);
        List<string> GetEmployeeNames(List<EmployeeStats> employeeStats);
    }
}