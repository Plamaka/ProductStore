using Meny_To_Meny_Relationship_in_MVC.Data;
using Meny_To_Meny_Relationship_in_MVC.Intefaces;
using Meny_To_Meny_Relationship_in_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Meny_To_Meny_Relationship_in_MVC.Repository
{
    public class PostRepo : IPost
    {
        protected readonly MenyToMenyContext _context;

        public PostRepo(MenyToMenyContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public Post GetById(int id)
        {
            return _context.Posts.Include(p => p.PostTags).ThenInclude(pt => pt.Tag).FirstOrDefault(m => m.Id == id);
        }

        public bool CreatePostTag(PostTag postTag)
        {
            _context.PostTags.Add(postTag);
            return Save();
        }

        public ICollection<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public bool Add(Post post)
        {
            _context.Posts.Add(post);
            return Save();
        }      

        public bool Delete(Post post)
        {
            _context.Posts.Remove(post);
            return Save();
        }

        public bool Update(Post post)
        {
            _context.Posts.Update(post);
            return Save();
        }

        public bool Save()
        {
            
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
