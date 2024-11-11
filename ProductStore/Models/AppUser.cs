using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Models
{
    public class AppUser : IdentityUser
    {

        public int? Pace { get; set; }

        public int? Mileage { get; set; }

    }
}
