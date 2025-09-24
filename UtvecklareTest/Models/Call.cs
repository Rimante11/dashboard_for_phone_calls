namespace UtvecklareTest.Models
{
    public class Call
    {
        public DateTime date { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }

    public class Employee
    {
        public string Name { get; set; } = "";
        public int Calls {  get; set; }
    }
}
