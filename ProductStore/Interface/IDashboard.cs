using ProductStore.Data;
using ProductStore.Models;

namespace ProductStore.Interface
{
    public interface IDashboard
    {
        Order GetOrderById(int id);

        List<Order> GetAllOrders();

        List<Order> GetAllUserOrders();

        bool Update(Order order);
            
        bool Save();

    }
}
