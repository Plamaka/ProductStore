using Meny_To_Meny_Relationship_in_MVC.Intefaces;
using Meny_To_Meny_Relationship_in_MVC.Models;
using Meny_To_Meny_Relationship_in_MVC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Meny_To_Meny_Relationship_in_MVC.Controllers
{
    public class PostController : Controller
    {
        protected readonly IPost _postRepo;
        protected readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostController(IPost postRepo, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _postRepo = postRepo;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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
                ModelState.AddModelError("", "Failed to create Post!");
                return View("Error", postVM);
            }

            var curUserID = _userManager.GetUserId(User);

            var post = new Post
            {
                Title = postVM.Title,
                Description = postVM.Description,
                UserId = curUserID
            };

            _postRepo.Add(post);

            foreach (var tagId in postVM.SelectedTagsIds)
            {
                var postTag = new PostTag
                {
                    PostId = post.Id,
                    TagId = tagId
                };
                _postRepo.CreatePostTag(postTag);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var model = _postRepo.GetById(id);
            var post = new DetailsPostViewModel
            {
                Id = id,
                Title = model.Title,
                Description = model.Description,
                Tags = model.PostTags.Select(pt => new TagViewModel
                {
                    Id = pt.Tag.Id,
                    Title = pt.Tag.Title
                }).ToList()
            }; 
            return View(post);
        }

        public IActionResult Edit(int id)
        {
            var model = _postRepo.GetById(id);
            if(model == null) { return View("Error"); }
            var vm = new EditPostViewModel
            {
                Id = id,
                Title = model.Title,
                Description = model.Description,
                SelectedTagIds = model.PostTags.Select(pt => pt.TagId).ToList(),
                Tags = _postRepo.GetAllTags().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditPostViewModel postVM)
        {
            if (id != postVM.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to create Tag!");
                return View("Error", postVM);
            }

            var post = _postRepo.GetById(id);

            if (post == null)
            {
                return NotFound();
            }

            post.Title = postVM.Title;
            post.Description = postVM.Description;

            var existingTags = post.PostTags.Select(pt => pt.TagId).ToList();
            var newTags = postVM.SelectedTagIds.Except(existingTags).ToList();
            var removedTags = existingTags.Except(postVM.SelectedTagIds).ToList();

            foreach (var tagId in newTags)
            {
                post.PostTags.Add(new PostTag { PostId = id, TagId = tagId });
            }

            foreach (var tagId in removedTags)
            {
                var postTag = post.PostTags.FirstOrDefault(pt => pt.TagId == tagId);
                if (postTag != null)
                {
                    post.PostTags.Remove(postTag);
                }
            }

            _postRepo.Update(post);               

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var model = _postRepo.GetById(id);
            if (model == null) { return View("Error"); }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var post = _postRepo.GetById(id);
            if (post == null) return View("Error");

            var removedTags = post.PostTags.Select(pt => pt.TagId).ToList();
            foreach (var tagId in removedTags)
            {
                var postTag = post.PostTags.FirstOrDefault(pt => pt.TagId == tagId);
                if (postTag != null)
                {
                    post.PostTags.Remove(postTag);
                }
            }

            _postRepo.Delete(post);
            return RedirectToAction("Index");
        }
    }
}
