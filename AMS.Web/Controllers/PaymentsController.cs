﻿using System;
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
        private readonly INotificationService notificationService;
        private readonly IApartmentService apartmentService;

        public PaymentsController(IPaymentService paymentService, INotificationService notificationService,
            IApartmentService apartmentService, UserManager<User> userManager)
        {
            this.paymentService = paymentService;
            this.notificationService = notificationService;
            this.apartmentService = apartmentService;
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
                var apartment = apartmentService.GetApartmentWithUsers(payment.ApartmentId);
                var notificationMessage = $"You have new payment. Sum: {payment.Sum}$.";
                foreach (var apartmentInhabitant in apartment.Inhabitants)
                {
                    await notificationService.Create(notificationMessage, apartmentInhabitant.Id);
                }

                await paymentService.AddPaymentAsync(payment);

                return RedirectToAction("ChangePayments", new { apartmentId = createApartmentViewModel.ApartmentId });
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
            if (changePaymentViewModel.NewStatus == PaymentStatus.Paid)
            {
                var apartment = apartmentService.GetApartmentWithUsers(changePaymentViewModel.ApartmentId);
                var notificationMessage = "Your manager confirmed the rental payment.";
                foreach (var user in apartment.Inhabitants)
                {
                    await notificationService.Create(notificationMessage, user.Id);
                }
            }

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
