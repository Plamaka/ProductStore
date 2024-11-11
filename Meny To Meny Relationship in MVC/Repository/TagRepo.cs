using Meny_To_Meny_Relationship_in_MVC.Data;
using Meny_To_Meny_Relationship_in_MVC.Intefaces;
using Meny_To_Meny_Relationship_in_MVC.Models;

namespace Meny_To_Meny_Relationship_in_MVC.Repository
{
    public class TagRepo : ITag
    {
        protected readonly MenyToMenyContext _context;

        public TagRepo(MenyToMenyContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetAll()
        {
           return _context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
           return _context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public ICollection<Post> GetTagByPost(int tagId)
        {
            return _context.PostTags.Where(pt => pt.TagId == tagId).Select(pt => pt.Post).ToList();
        }

        public bool Add(Tag tag)
        {
             _context.Tags.Add(tag);
            return Save();
        }

        public bool Delete(Tag tag)
        {
            _context.Remove(tag);
            return Save();
        }

        public bool Update(Tag tag)
        {
            _context.Update(tag);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
