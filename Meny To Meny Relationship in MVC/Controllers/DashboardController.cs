using Meny_To_Meny_Relationship_in_MVC.Intefaces;
using Meny_To_Meny_Relationship_in_MVC.Models;
using Meny_To_Meny_Relationship_in_MVC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Meny_To_Meny_Relationship_in_MVC.Controllers
{
    public class DashboardController : Controller
    {
        protected readonly IDashboard _dashboardRepo;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboard dashboardRepo, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardRepo = dashboardRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var userPosts = _dashboardRepo.GetAllUserPosts();
            var DashboardVM = new DashboardViewModel()
            {
                Posts = userPosts
            };
            return View(DashboardVM);
        }
    }
}
