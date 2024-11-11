using Microsoft.AspNetCore.Mvc;
using ProductStore.Interface;

namespace ProductStore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboardRepository;

        public DashboardController(IDashboard dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
