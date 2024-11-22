using ProductStore.Data;
using ProductStore.Interface;
using ProductStore.Models;

namespace ProductStore.Repository
{
    public class DashboardRepository : IDashboard
    {
        protected readonly StoreContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(StoreContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public List<Order> GetAllUserOrders()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userOrders = _context.Orders.Where(p => p.Customer.Id == curUser.ToString());
            return userOrders.ToList();

        }


    }
}
