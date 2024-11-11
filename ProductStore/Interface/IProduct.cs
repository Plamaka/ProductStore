using ProductStore.Models;

namespace ProductStore.Interface
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetByIdAsync(int Id);

        bool Add(Product product);

        bool Update(Product product);

        bool Delete(Product product);

        bool Save();

    }
}
