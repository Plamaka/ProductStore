using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Column(TypeName="VARCHAR(200)")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "VARCHAR(200)")]
        public string Vendor { get; set; } = null!;

        [Column(TypeName = "VARCHAR(45)")]
        public string ItemType { get; set; } = null!;

        [Column(TypeName = "VARCHAR(45)")]
        public string location { get; set; } = null!;

        [Column(TypeName = "VARCHAR(45)")]
        public string Unit { get; set; } = null!;
    }
}
