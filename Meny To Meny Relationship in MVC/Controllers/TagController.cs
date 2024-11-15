using Meny_To_Meny_Relationship_in_MVC.Intefaces;
using Meny_To_Meny_Relationship_in_MVC.Models;
using Meny_To_Meny_Relationship_in_MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Meny_To_Meny_Relationship_in_MVC.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class TagController : Controller
    {
        protected readonly ITag _tagRepo;

        public TagController(ITag tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public IActionResult Index()
        {
            var tagVM = _tagRepo.GetAll().Select(x => new TagViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();
            return View(tagVM);
        }

        public IActionResult Create()
        {
            var tagVM = new CreateTagViewModel();
            return View(tagVM);
        }

        [HttpPost]
        public IActionResult Create(CreateTagViewModel tagVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to create Tag!");
                return View("Error", tagVM);
            }

            Tag vm = new Tag
            {
                
                Title = tagVM.Title
            };

            _tagRepo.Add(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            var model = _tagRepo.GetById(Id);
            if (model == null) return View("Error");
            var tagVM = new TagViewModel
            {
                Id = model.Id,
                Title = model.Title
            };

            return View(tagVM);
        }

        public IActionResult Edit(int id)
        {
            var model = _tagRepo.GetById(id);
            if (model == null) return View("Error");
            var tagVM = new TagViewModel
            {
                Title = model.Title
            };

            return View(tagVM);
        }

        [HttpPost]
        public IActionResult Edit(TagViewModel tagVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit product!");
                return View("Error", tagVM);
            }

            var vm = new Tag
            {
                Id= tagVM.Id,
                Title = tagVM.Title
            };

            _tagRepo.Update(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var model = _tagRepo.GetById(id);
            if (model == null) return View("Error");
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteTag(int id)
        {
            var model = _tagRepo.GetById(id);
            if (model == null) return View("Error");
            _tagRepo.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
