using ProductStore.Data;
using ProductStore.Models;

namespace ProductStore.Interface
{
    public interface IDashboard
    {
        Task<List<Order>> GetAllOrders();
    }
}
