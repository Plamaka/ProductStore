using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string? Name { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal Price{ get; set; } 
    }

}
