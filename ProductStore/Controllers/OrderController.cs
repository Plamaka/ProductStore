using Microsoft.AspNetCore.Mvc;
using ProductStore.Interface;
using ProductStore.Models;
using ProductStore.ViewModels;

namespace ProductStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _orderRepository;
        private readonly IHttpContextAccessor _httpContentAccessor;

        public OrderController(IOrder orderRepository, IHttpContextAccessor httpContentAccessor)
        {
            _orderRepository = orderRepository;
            _httpContentAccessor = httpContentAccessor;
        }

        public IActionResult Create(int productId)
        {
            if (productId == null)
            {
                return View("Error");
            }
            var curUser = _httpContentAccessor.HttpContext?.User.GetUserId();
            var product = _orderRepository.GetProductById(productId);
            var orderVM = new CreateOrderViewModel
            {
                CustomerId = curUser,
                OrderPlaced = DateTime.Now.ToLocalTime(),
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return View(orderVM);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel orderVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Order failed!");
                return View("Error", orderVM);
            }


            var order = new Order
            {
                OrderPlaced = orderVM.OrderPlaced,
                CustomerId = orderVM.CustomerId
            };

            _orderRepository.Add(order);

            var orderDetail = new OrderDetail
            {
                ProductId = orderVM.ProductId,
                OrderId = order.Id,
                Quantity = orderVM.Quantity
            };

            _orderRepository.AddOrderDetail(orderDetail);

            return RedirectToAction("Index", "Product");
        }

        
        public IActionResult Edit(int id)
        {
            var order = _orderRepository.GetProductById(id);
            var orderVM = new CreateOrderViewModel();
            return RedirectToAction();
        }
    }
}
