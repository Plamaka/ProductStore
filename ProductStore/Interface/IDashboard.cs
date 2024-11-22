using ProductStore.Data;
using ProductStore.Models;

namespace ProductStore.Interface
{
    public interface IDashboard
    {
        List<Order> GetAllOrders();

        List<Order> GetAllUserOrders();
    }
}
