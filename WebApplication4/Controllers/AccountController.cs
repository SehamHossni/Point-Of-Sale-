using BLL.ViewModel;
using Dal.Entities;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager ,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        // Register As Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(UserRegisterViewModel UserVM)
        {
            if (ModelState.IsValid)
            { 
             ApplicationUser userModel = new ApplicationUser();
             userModel.UserName= UserVM.UserName;
             userModel.PasswordHash = UserVM.Password;
             userModel.Address = UserVM.Address;
             IdentityResult result = await userManager.CreateAsync(userModel,UserVM.Password);
                if (result.Succeeded)
                {
                   
                    await signInManager.SignInAsync(userModel,false); //when session end user login again
                    return RedirectToAction("Index", "Order");
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        // Register As Normal User
        [Authorize(Roles = "Admin")]
        public IActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserRegister(UserRegisterViewModel UserVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = UserVM.UserName;
                userModel.PasswordHash = UserVM.Password;
                userModel.Address = UserVM.Address;
                IdentityResult result = await userManager.CreateAsync(userModel, UserVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    await signInManager.SignInAsync(userModel, false); //when session end user login again
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
       
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userLogVM)
        {
            if (ModelState.IsValid)
            {
              ApplicationUser UserModel = await userManager.FindByNameAsync(userLogVM.UserName);
                if (UserModel != null)
                {
                   await signInManager.PasswordSignInAsync(UserModel,userLogVM.Password,userLogVM.RememberMe,false);
                   return RedirectToAction("Index", "Order");
                }
                else 
                {
                    ModelState.AddModelError("","User Name or Password Wrong");
                }
            }
            return View();
        }


        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
