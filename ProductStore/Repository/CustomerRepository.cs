using Microsoft.EntityFrameworkCore;
using ProductStore.Data;
using ProductStore.Interface;
using ProductStore.Models;

namespace ProductStore.Repository
{
    public class CustomerRepository : ICustomer
    {
        private readonly StoreContext _context;

        public CustomerRepository(StoreContext context) 
        {
            _context = context;
        }

        public bool Add(Customer customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool Delete(Customer customer)
        {
            _context.Remove(customer);
            return Save();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdCustomer(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool Update(Customer customer)
        {
            _context.Update(customer);
            return Save();
        }
    }
}
