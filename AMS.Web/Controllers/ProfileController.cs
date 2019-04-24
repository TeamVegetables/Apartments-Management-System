using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Web.ViewModels.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;

        public ProfileController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ChangeRoleViewModel changeRoleViewModel)
        {
            var user = userManager.Users.ToList().Select(u => u).FirstOrDefault(u => u.Id == changeRoleViewModel.UserId);
            if (user == null) return View();
            await userManager.RemoveFromRoleAsync(user, changeRoleViewModel.OldRole);
            await userManager.AddToRoleAsync(user, changeRoleViewModel.NewRole);

            return View();
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(createUserViewModel.Email);

                if (user != null)
                {
                    ModelState.AddModelError("Error", "User with this email already exists");
                    return View(createUserViewModel);
                }

                user = new User
                {
                    UserName = createUserViewModel.Email,
                    Email = createUserViewModel.Email,
                    FirstName = createUserViewModel.FirstName,
                    LastName = createUserViewModel.LastName,
                    DateOfBirth = createUserViewModel.DateOfBirth
                };

                var result = await userManager.CreateAsync(user, createUserViewModel.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Error", "User creating error. Try again later.");
                    return View(createUserViewModel);
                }

                result = await userManager.AddToRoleAsync(user, createUserViewModel.Role);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Error", "User creating error. Try again later.");
                    return View(createUserViewModel);
                }

                return RedirectToAction("Index");
            }

            return View(createUserViewModel);
        }
    }
}