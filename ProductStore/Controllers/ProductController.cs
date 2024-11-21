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

        public IActionResult Create() 
        {
            var productVM = new ProductViewModel();
            return View(productVM); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                return View(productVm);
            }

            var product = new Product
            {
                Name = productVm.Name,
                Price = productVm.Price
            };

             _productRepository.Add(product);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            if (product == null) { return View("Error"); }
            var productVM = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
            return View(productVM);
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
        public IActionResult Edit(int id, EditProductViewModel productVM)
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

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) { return View("Error"); }
            var productVM = new EditProductViewModel
            {
                Id= id,
                Name = product.Name,
                Price = product.Price
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Delete(EditProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Faild to Delete product!");
                return View("Error", productVM);
            }

            var product = new Product
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Price = productVM.Price
            };

            _productRepository.Delete(product);
            return RedirectToAction("Index");
        }
    }
}
