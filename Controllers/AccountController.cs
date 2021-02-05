using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MMGDH_Blog.ViewModels;

namespace MMGDH_Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.PassWord, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Index");
                }
            }
            ModelState.AddModelError("", "用户名或密码不正确");
            return View(loginViewModel);
        }
        public async Task<IActionResult> outLogin()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Index");
        }

        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var User = new IdentityUser
                {
                    UserName = registerViewModel.UserName
                };
                var result = await _userManager.CreateAsync(User, registerViewModel.PassWord);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View();
        }
    }
}