using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductStore.Data;
using ProductStore.Interface;
using ProductStore.Models;

namespace ProductStore.Repository
{
    public class ProductRepository : IProduct
    {

        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }  

        public bool Add(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
           _context.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<Product> GetByIdAsync(int Id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product product)
        {
            _context.Update(product);
            return Save();

        }
    }
}
