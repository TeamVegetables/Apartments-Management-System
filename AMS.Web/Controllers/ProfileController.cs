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
        public async Task<IActionResult> ChangeRole(ChangeRoleViewModel changeRoleViewModel)
        {
            var user = userManager.Users.ToList().Select(u => u).FirstOrDefault(u => u.Id == changeRoleViewModel.UserId);
            if (user == null) return View("Index");
            await userManager.RemoveFromRoleAsync(user, changeRoleViewModel.OldRole);
            await userManager.AddToRoleAsync(user, changeRoleViewModel.NewRole);

            return View("Index");
        }
    }
}