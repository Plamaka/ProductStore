using Microsoft.AspNetCore.Mvc;
using ProductStore.Interface;
using ProductStore.ViewModels;

namespace ProductStore.Controllers
{
    public class DashboardController : Controller
    {
        protected readonly IDashboard _dashboardRepo;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboard dashboardRepo, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardRepo = dashboardRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var userOrders = _dashboardRepo.GetAllUserOrders();
            var DashboardVM = new DashboardViewModel()
            {
                Orders = userOrders
            };
            return View(DashboardVM);
        }
    }
}
