using ProductStore.Models;

namespace ProductStore.ViewModels
{
    public class CreateOrderViewModel
    {

        public DateTime OrderPlaced { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
