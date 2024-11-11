using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductStore.Data;
using ProductStore.Models;
using ProductStore.ViewModels;

namespace ProductStore.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly StoreContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManeger, StoreContext context)
        {
            _context = context;
            _signInManager = signInManeger;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) 
            {
                return View(loginViewModel);
            }

            //User is found Check password
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if(passwordCheck)
                {
                    //password is corect sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Product");
                    }
                }
                //password is incorect
                TempData["Error"] = "Wrong password. Please, try again";
                return View(loginViewModel);
            }
            //User not found
            TempData["Error"] = "Wrong email. Please, try again";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var respons = new RegisterViewModel();
            return View(respons);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email alredi has a account";
                return View(registerViewModel);
            }

            var newUser = new AppUser
            {
                Mileage = 0,
                Pace = 0,
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                TempData["Error"] = "Password Requires Non Alphanumeric";
                return View(registerViewModel);
            }

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
