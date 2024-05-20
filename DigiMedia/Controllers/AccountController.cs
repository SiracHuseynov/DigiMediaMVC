using DigiMedia.Core.Models;
using DigiMedia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigiMedia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()  
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVm memberRegisterVm)
        {
            var appUser = await _userManager.FindByNameAsync(memberRegisterVm.Username);

            if(appUser != null)
            {
                ModelState.AddModelError("Username", "Username already exist");
                return View();
            }

            appUser = await _userManager.FindByEmailAsync(memberRegisterVm.Email);

            if(appUser != null)
            {
                ModelState.AddModelError("Email", "Email already exist");
                return View();
            }

            appUser = new AppUser
            {
                UserName = memberRegisterVm.Username,
                FullName = memberRegisterVm.FullName,
                Email = memberRegisterVm.Email
            };

            var result = await _userManager.CreateAsync(appUser, memberRegisterVm.Password);

            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            return RedirectToAction("Login", "Account");

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginVm memberLoginVm)
        {
            var appUser = await _userManager.FindByNameAsync(memberLoginVm.Username);

            if(appUser == null)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, memberLoginVm.Password, false, false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
