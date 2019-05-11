using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly UserManager<User> userManager;

        public NotificationsController(INotificationService notificationService, UserManager<User> userManager)
        {
            this.notificationService = notificationService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var notifications = notificationService.GetByUserId(user.Id);
            return View(notifications);
        }

        [HttpPost]
        public IActionResult Dismiss(int notificationId)
        {
            notificationService.Dismiss(notificationId);
            return RedirectToAction("Index");
        }
    }
}