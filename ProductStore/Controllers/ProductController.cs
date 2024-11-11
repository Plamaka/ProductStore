using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductStore.Data;
using ProductStore.Interface;
using ProductStore.Models;
using ProductStore.Repository;
using ProductStore.ViewModels;

namespace ProductStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _productRepository;

        public ProductController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> product = await _productRepository.GetAll();
            return View(product);
        }

        public async Task<IActionResult> Detail(int id) 
        {
            Product product = await _productRepository.GetByIdAsync(id);
            return View(product);
        }

        public IActionResult Create() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _productRepository.Add(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var stok = await _productRepository.GetByIdAsync(id);
            if (stok == null) return View("Error");
            var productVM = new EditProductViewModel
            {
                Name = stok.Name,
                Price = stok.Price
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Faild to Edit product!");
                return View("Error", productVM);
            }

            var srok = new Product
            {
                Id = id,
                Name = productVM.Name,
                Price = productVM.Price
            };

            _productRepository.Update(srok);
            return RedirectToAction("Index");
        }
    }
}
