using Meny_To_Meny_Relationship_in_MVC.Data;
using Meny_To_Meny_Relationship_in_MVC.Intefaces;
using Meny_To_Meny_Relationship_in_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Meny_To_Meny_Relationship_in_MVC.Repository
{
    public class DashboardRepo : IDashboard
    {
        protected readonly MenyToMenyContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepo(MenyToMenyContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Post> GetAllUserPosts()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userPosts = _context.Posts.Where(p => p.User.Id == curUser.ToString());
            return userPosts.ToList();

        }

    }
}
