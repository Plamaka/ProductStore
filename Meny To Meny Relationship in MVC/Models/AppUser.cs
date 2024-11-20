using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Meny_To_Meny_Relationship_in_MVC.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; } = null!;
    }
}
