using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meny_To_Meny_Relationship_in_MVC.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; } 

        [Column(TypeName ="NVARCHAR(50)")]
        public string Title { get; set; } = null!;

        [Column(TypeName = "NVARCHAR(450)")]
        public string Description { get; set; } = null!;

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
