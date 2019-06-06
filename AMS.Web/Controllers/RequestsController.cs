using System;
using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.ViewModels.Request;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Web.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IApartmentService _apartmentService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RequestsController(IRequestService requestService, UserManager<User> userManager, IMapper mapper, IApartmentService apartmentService)
        {
            _requestService = requestService;
            _userManager = userManager;
            _mapper = mapper;
            _apartmentService = apartmentService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(CreateRequestViewModel createRequestModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                createRequestModel.InitiatorId = user.Id;
                createRequestModel.ResolverId = user.ManagerId;
                createRequestModel.Initiated = DateTime.Now;
                createRequestModel.Status = RequestStatus.Initiated;

                var request = _mapper.Map<CreateRequestViewModel, Request>(createRequestModel);

                await _requestService.AddRequestAsync(request);

                return RedirectToAction("Index");
            }

            return View(createRequestModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRequestStatus(UpdateRequestStatusViewModel updateStatusModel)
        {
            var requestEntity = await _requestService.GetRequestAsync(updateStatusModel.Id);

            if (requestEntity == null)
            {
                return View("Index");
            }

            requestEntity.Completed = updateStatusModel.Status == RequestStatus.Resolved
                ? (DateTime?) DateTime.Now
                : null;
            requestEntity.Status = updateStatusModel.Status;

            await _requestService.UpdateRequestAsync(requestEntity);

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Extend(UpdateRequestStatusViewModel requestViewModel)
        {
            var user = await _userManager.FindByIdAsync(requestViewModel.UserId);
            user.RentEndDate = user.RentEndDate.Value.AddMonths(2);
            await _userManager.UpdateAsync(user);
            var request = await _requestService.GetRequestAsync(requestViewModel.Id);
            await _requestService.RemoveRequestAsync(request);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Decline(UpdateRequestStatusViewModel requestViewModel)
        {
            var request = await _requestService.GetRequestAsync(requestViewModel.Id);
            await _requestService.RemoveRequestAsync(request);
            return RedirectToAction("Index");
        }
    }
}