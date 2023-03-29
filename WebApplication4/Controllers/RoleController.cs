using BLL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> rolemanager)
        {
            this.roleManager = rolemanager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel RoleVM)
        {
            if (ModelState.IsValid)
            {
              IdentityRole role = new IdentityRole();
              role.Name = RoleVM.RoleName;
              IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(RoleVM);
        }
    }
}
