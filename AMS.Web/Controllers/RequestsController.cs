using System;
using System.Collections.Generic;
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
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RequestsController(IRequestService requestService, UserManager<User> userManager, IMapper mapper)
        {
            _requestService = requestService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            
            var requestsViewModel = _mapper.Map <ICollection<Request>, IEnumerable<RequestViewModel>>(requests);

            return View("RequestList" , requestsViewModel);
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
    }
}