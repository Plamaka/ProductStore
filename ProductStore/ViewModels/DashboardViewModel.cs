using ProductStore.Models;

namespace ProductStore.ViewModels
{
    public class DashboardViewModel
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
