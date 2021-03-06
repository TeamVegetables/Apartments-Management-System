﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.ViewModels.Apartments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Web.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IApartmentService apartmentService;
        private readonly UserManager<User> userManager;
        private readonly IRequestService requestService;

        public ApartmentsController(IApartmentService apartmentService, UserManager<User> userManager, IRequestService requestService)
        {
            this.apartmentService = apartmentService;
            this.userManager = userManager;
            this.requestService = requestService;
        }

        public async Task<IActionResult> Index()
        {
            var apartments = await apartmentService.GetAllApartmentsAsync();
            return View(apartments);
        }

        [HttpGet]
        public IActionResult CreateApartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartment(CreateApartmentViewModel createApartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var apartment = new Apartment
                {
                    Capacity = createApartmentViewModel.Capacity,
                    Status = createApartmentViewModel.Status,
                    Title = createApartmentViewModel.Title
                };
                await apartmentService.AddApartmentAsync(apartment);

                return RedirectToAction("Index");
            }

            return View(createApartmentViewModel);
        }

        [HttpGet]
        public IActionResult RecentlyFreeApartments()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageInhabitants(int apartmentId)
        {
            var viewModel = new ManageInhabitantViewModel {ApartmentId = apartmentId};
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddInhabitant(ManageInhabitantViewModel viewModel)
        {
            var apartment = await apartmentService.GetApartmentAsync(viewModel.ApartmentId);
            var user = userManager.Users.Select(u => u).FirstOrDefault(u => u.Id == viewModel.InhabitantId);
            if (user == null)
            {
                return BadRequest();
            }

            if (apartment.Inhabitants == null)
            {
                apartment.Inhabitants = new List<User>();
            }

            apartment.Inhabitants.Add(user);
            apartment.Busy++;
            apartment.Status = apartment.Busy >= apartment.Capacity ? ApartmentStatus.Busy : ApartmentStatus.Available;
            user.RentEndDate = viewModel.RentEndDate;
            user.Apartment = apartment;
            user.ApartmentId = apartment.Id;
            await userManager.UpdateAsync(user);
            await apartmentService.UpdateApartmentAsync(apartment);
            return RedirectToAction("ManageInhabitants", new {apartmentId = viewModel.ApartmentId});
        }

        [HttpPost]
        public async Task<IActionResult> ExtendRent()
        {
            var user = await userManager.GetUserAsync(User);
            var request = new Request
            {
                ApartmentId = user.ApartmentId,
                Initiated = DateTime.Now,
                Message = "Please, extend my rent!",
                InitiatorId = user.Id,
                ResolverId = user.ManagerId,
                Status = RequestStatus.Initiated
            };
            await requestService.AddRequestAsync(request);

            return RedirectToAction("GetApartmentByInhabitantId");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveInhabitant(ManageInhabitantViewModel viewModel)
        {
            var apartment = await apartmentService.GetApartmentAsync(viewModel.ApartmentId);
            var user = userManager.Users.Select(u => u).FirstOrDefault(u => u.Id == viewModel.InhabitantId);
            if (user == null)
            {
                return BadRequest();
            }

            apartment.Inhabitants.Remove(user);
            apartment.Busy--;
            apartment.Status = apartment.Busy >= apartment.Capacity ? ApartmentStatus.Busy : ApartmentStatus.Available;
            user.Apartment = null;
            user.ApartmentId = null;
            await userManager.UpdateAsync(user);
            await apartmentService.UpdateApartmentAsync(apartment);
            return RedirectToAction("ManageInhabitants", new {apartmentId = viewModel.ApartmentId});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int apartmentId)
        {
            var apartment = await apartmentService.GetApartmentAsync(apartmentId);
            if (apartment != null)
            {
                await apartmentService.RemoveApartmentAsync(apartment);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetApartmentByInhabitantId()
        {
            var user = await userManager.GetUserAsync(User);
            Apartment apartment = null;
            if (user.ApartmentId.HasValue)
            {
                 apartment = await apartmentService.GetApartmentAsync(user.ApartmentId.Value);
            }

            return View(apartment);
        }
    }
}
