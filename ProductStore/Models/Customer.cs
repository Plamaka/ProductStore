using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Models
{
    public class Customer : IdentityUser
    {
        [Column(TypeName = "NVARCHAR(50)")]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "NVARCHAR(50)")]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Address { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        public string? Phone{ get; set; }

        public ICollection<Order> Orders { get; set; } = null!;
    }
}
