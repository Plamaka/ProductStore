using Microsoft.AspNetCore.Mvc;
using ProductStore.Interface;
using ProductStore.Models;
using ProductStore.ViewModels;

namespace ProductStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _orderRepository;

        public OrderController(IOrder orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Create(int productId)
        {
            if (productId == null)
            {
                return View("Error");
            }
            var product = _orderRepository.GetProductById(productId);
            var orderVM = new CreateOrderViewModel
            {              
                OrderPlaced = DateTime.Now.ToLocalTime(),
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return View(orderVM);
        }

        [HttpPost]
        public IActionResult Create( CreateOrderViewModel orderVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Order failed!");
                return View("Error", orderVM);
            }


            var order = new Order
            {
                OrderPlaced = orderVM.OrderPlaced,
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
    }
}
