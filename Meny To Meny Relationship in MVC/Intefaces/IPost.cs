using Meny_To_Meny_Relationship_in_MVC.Models;

namespace Meny_To_Meny_Relationship_in_MVC.Intefaces
{
    public interface IPost
    {
        IEnumerable<Post> GetAll();
        
        Post GetById(int id);

        ICollection<Tag> GetAllTags();

        bool CreatePostTag(PostTag postTag);

        bool Add(Post post);

        bool Update(Post post);

        bool Delete(Post post);

        bool Save();
    }
}
