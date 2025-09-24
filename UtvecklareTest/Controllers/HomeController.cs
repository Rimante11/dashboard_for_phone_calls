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
            return View(calls);
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
