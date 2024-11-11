using ProductStore.Data;
using ProductStore.Interface;
using ProductStore.Models;

namespace ProductStore.Repository
{
    public class DashboardRepository 
    {
        private readonly StoreContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(StoreContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


    }
}
