using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.ViewModels.Payments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AMS.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly UserManager<User> userManager;

        public PaymentsController(IPaymentService paymentService, UserManager<User> userManager)
        {
            this.paymentService = paymentService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreatePayment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentViewModel createApartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    
                };
                await paymentService.AddPaymentAsync(payment);

                return RedirectToAction("Index");
            }

            return View(createApartmentViewModel);
        }

        public async Task<IActionResult> Index()
        {
            var payments = await paymentService.GetAllPaymentsAsync();
            return View(payments);
        }
    }
}
