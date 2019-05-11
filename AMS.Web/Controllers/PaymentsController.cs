using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult ChangePayments(int apartmentId)
        {
            var changePaymentViewModel = new ChangePaymentViewModel
            {
                ApartmentId = apartmentId
            };

            return View(changePaymentViewModel);
        }

        [HttpPost]
        public async  Task<IActionResult> ChangePayments(ChangePaymentViewModel changePaymentViewModel)
        {
            var payment = await paymentService.GetPaymentAsync(changePaymentViewModel.PaymentId);
            payment.DeadLine = changePaymentViewModel.DeadLine;
            payment.Status = changePaymentViewModel.NewStatus;
            await paymentService.UpdatePaymentAsync(payment);

            return View(changePaymentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> InhabitantChangePayments()
        {
            var user = await userManager.GetUserAsync(User);
            var payments = new List<Payment>();
            if (user.ApartmentId.HasValue)
            {
                payments = (await paymentService.GetPaymentsByApartment(user.ApartmentId.Value)).ToList();
            }

            return View(payments);
        }

        [HttpPost]
        public async Task<IActionResult> InhabitantChangePayments(int paymentId)
        {
            var payment = await paymentService.GetPaymentAsync(paymentId);
            payment.Completed = DateTime.Now;
            await paymentService.UpdatePaymentAsync(payment);
            await paymentService.ChangeStatus(payment);

            return RedirectToAction("InhabitantChangePayments");
        }


        public async Task<IActionResult> Index()
        {
            var payments = await paymentService.GetAllPaymentsAsync();
            return View(payments);
        }
    }
}
