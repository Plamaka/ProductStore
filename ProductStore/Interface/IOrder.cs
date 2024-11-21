using ProductStore.Models;

namespace ProductStore.Interface
{
    public interface IOrder
    {
        Product GetProductById(int id);

        bool AddOrderDetail(OrderDetail order);

        bool Add(Order order);

        bool Save();
    }
}
