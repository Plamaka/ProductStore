using Meny_To_Meny_Relationship_in_MVC.Data;
using Meny_To_Meny_Relationship_in_MVC.Models;

namespace Meny_To_Meny_Relationship_in_MVC.Intefaces
{
    public interface ITag
    {
        IEnumerable<Tag> GetAll();

        Tag GetById(int id);

        ICollection<Post> GetTagByPost(int tagId);

        bool Add(Tag tag);

        bool Update(Tag tag);

        bool Delete(Tag tag);

        bool Save();
    }
}
