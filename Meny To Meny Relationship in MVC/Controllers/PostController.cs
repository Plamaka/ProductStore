using Meny_To_Meny_Relationship_in_MVC.Intefaces;
using Meny_To_Meny_Relationship_in_MVC.Models;
using Meny_To_Meny_Relationship_in_MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Meny_To_Meny_Relationship_in_MVC.Controllers
{
    public class PostController : Controller
    {
        protected readonly IPost _postRepo;

        public PostController(IPost postRepo)
        {
            _postRepo = postRepo;
        }
        public IActionResult Create()
        {
            var postVM = new CreatePostViewModel
            {
                Tags = _postRepo.GetAllTags().ToList()
            };
            return View(postVM);
        }

        [HttpPost]
        public IActionResult Create(CreatePostViewModel postVM)
        {
            if (!ModelState.IsValid)
            {
                postVM.Tags = _postRepo.GetAllTags().ToList();
                return View(postVM);
            }

            var post = new Post
            {
                Title = postVM.Title,
               Description = postVM.Description
            };

            _postRepo.Add(post);

            foreach (var tagId in postVM.SelectedTagsIds)
            {
                var postTags = new PostTag
                {
                    PostId = post.Id,
                    TagId = tagId
                };
                _postRepo.CreatePostTag(postTags);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var model = _postRepo.GetAll().Select(x => new PostViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description                
            }).ToList();

            return View(model);
        }
    }
}
