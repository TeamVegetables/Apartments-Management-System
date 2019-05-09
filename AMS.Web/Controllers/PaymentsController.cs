using System;
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
        public IActionResult CreatePayment(int apartmentId)
        {
            var viewModel = new CreatePaymentViewModel { ApartmentId = apartmentId };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentViewModel createApartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    Sum = createApartmentViewModel.Sum,
                    DeadLine = createApartmentViewModel.DeadLine,
                    Initiated = DateTime.Now,
                    ApartmentId = createApartmentViewModel.ApartmentId,
                    Status = PaymentStatus.Waiting
                };
                await paymentService.AddPaymentAsync(payment);

                return RedirectToAction("Index");
            }

            return View(createApartmentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments(int apartmentId)
        {
            var payments = await paymentService.GetPaymentsByApartment(apartmentId);
            return  View(payments);
        }

        public async Task<IActionResult> Index()
        {
            var payments = await paymentService.GetAllPaymentsAsync();
            return View(payments);
        }
    }
}
