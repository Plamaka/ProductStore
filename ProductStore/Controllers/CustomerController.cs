using Microsoft.AspNetCore.Mvc;
using ProductStore.Data;
using ProductStore.Interface;
using ProductStore.Models;
using ProductStore.Repository;
using ProductStore.ViewModels;

namespace ProductStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _customerRepository;

        public CustomerController(ICustomer customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Customer> customer = await _customerRepository.GetAll();
            return View(customer);
        }

        public async Task<IActionResult> Detail(string id)
        {
            Customer customerId = await _customerRepository.GetByIdCustomer(id);
            var customerVM = new DetailsCustomerViewModel
            {
                Username = customerId.UserName,
                Email = customerId.Email,
                FirstName = customerId.FirstName,
                LastName = customerId.LastName,
                Address = customerId.Address,
                PhoneNumber = customerId.Phone
            };
            return View(customerVM);
        }
    }
}
