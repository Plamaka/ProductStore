using Microsoft.AspNetCore.Mvc;
using ProductStore.Interface;
using ProductStore.ViewModels;

namespace ProductStore.Controllers
{
    public class DashboardController : Controller
    {
        protected readonly IDashboard _dashboardRepository;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboard dashboardRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var orders = _dashboardRepository.GetAllOrders();
            var userOrders = _dashboardRepository.GetAllUserOrders();
            var DashboardVM = new DashboardViewModel()
            {
                GetAllOrders = orders,
                Orders = userOrders
            };
            return View(DashboardVM);
        }
    }
}
