namespace Meny_To_Meny_Relationship_in_MVC.ViewModel
{
    public class DetailsPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<TagViewModel> Tags { get; set; }
    }
}
