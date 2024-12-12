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

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Where(o => o.OrderFulfulled == null).ToList();
        }

        public List<Order> GetAllUserOrders()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userOrders = _context.Orders.Where(p => p.Customer.Id == curUser.ToString());
            return userOrders.ToList();
        }

        public bool Update(Order order)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.Id == order.Id);

            if (existingOrder == null)
                return false;

            existingOrder.OrderFulfulled = order.OrderFulfulled;
            existingOrder.OrderPlaced = order.OrderPlaced;
            existingOrder.CustomerId = order.CustomerId;

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
