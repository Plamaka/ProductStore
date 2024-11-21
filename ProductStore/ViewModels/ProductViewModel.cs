using ProductStore.Models;

namespace ProductStore.ViewModels
{
    public class ProductViewModel
    {
        IEnumerable<Product> Products { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
