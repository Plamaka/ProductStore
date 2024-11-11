namespace Meny_To_Meny_Relationship_in_MVC.Models
{
    public class PostTag
    {
        public int PostId { get; set; }

        public int TagId { get; set; }

        public Post Post { get; set; } = new Post();

        public Tag Tag { get; set; } = new Tag();
    }
}
