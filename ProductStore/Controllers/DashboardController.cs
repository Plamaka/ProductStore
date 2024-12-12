using Microsoft.AspNetCore.Mvc;
using ProductStore.Interface;
using ProductStore.Models;
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

        [HttpPost]
        public IActionResult Index(int id)
        {
            var order = _dashboardRepository.GetOrderById(id);

            order.OrderFulfulled = DateTime.Now.ToLocalTime();
            _dashboardRepository.Update(order);
            return RedirectToAction("Index");
        }
    }
}
