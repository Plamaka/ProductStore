using Meny_To_Meny_Relationship_in_MVC.Models;

namespace Meny_To_Meny_Relationship_in_MVC.ViewModel
{
    public class EditPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Tag>? Tags { get; set; }

        public List<int>? SelectedTagIds { get; set; }
    }
}
