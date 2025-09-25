using Microsoft.AspNetCore.Mvc;
using UtvecklareTest.Models;
using UtvecklareTest.Services.Interfaces;

namespace UtvecklareTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICallService _callService;

        public HomeController(ICallService callService)
        {
            _callService = callService;
        }

        public IActionResult Index()
        {
            var model = _callService.GetDashboardData();
            return View(model);
        }
    }
}
