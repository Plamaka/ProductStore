namespace ProductStore.ViewModels
{
    public class EditProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
