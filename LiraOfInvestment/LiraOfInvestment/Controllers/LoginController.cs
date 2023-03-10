using EntityLayer.Concrete;
using LiraOfInvestment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiraOfInvestment.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, false, true);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    foreach (var claim in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, claim));
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Çok fazla deneme yapıldı lütfen bekleyiniz");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                    return View(nameof(Login));
                }

            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel esv)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = esv.UserName,
                    Email = esv.Mail,
                    PhoneNumber=esv.Phone,
                    CustomerFirstName=esv.FirstName, 
                    CustomerLastName=esv.LastName,
                    CustomerCreatedTime=DateTime.Now,
                    CustomerStatus=true,

                };

                var result = await _userManager.CreateAsync(user, esv.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(esv);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
