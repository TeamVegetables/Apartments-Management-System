using System;
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
        public async Task<IActionResult> Index(ChangeUserViewModel changeUserViewModel)
        {
            var user = userManager.Users.ToList().Select(u => u).FirstOrDefault(u => u.Id == changeUserViewModel.UserId);
            if (user == null) return View();
            await userManager.RemoveFromRoleAsync(user, changeUserViewModel.OldRole);
            await userManager.AddToRoleAsync(user, changeUserViewModel.NewRole);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeManager(ChangeUserViewModel changeUserViewModel)
        {
            var user = userManager.Users.ToList().Select(u => u).FirstOrDefault(u => u.Id == changeUserViewModel.UserId);
            if (user == null) return RedirectToAction("Index");
            user.ManagerId = changeUserViewModel.ManagerId;
            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserActivation(ChangeUserViewModel changeUserViewModel)
        {
            var user = userManager.Users.ToList().Select(u => u).FirstOrDefault(u => u.Id == changeUserViewModel.UserId);
            if (user == null) return RedirectToAction("Index");
            user.IsLocked = !user.IsLocked;
            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
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