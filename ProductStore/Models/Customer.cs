namespace ProductStore.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone{ get; set; }

        public ICollection<Order> Orders { get; set; } = null!;

        public ICollection<AppUser> AppUsers { get; set; } = null!;
    }
}
