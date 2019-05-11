using System;
using System.Linq;
using System.Security.Claims;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.ViewModels.Request;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AMS.Web.Profiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile(UserManager<User> userManager)
        {
            CreateMap<Request, RequestViewModel>()
                .ForMember(dest => dest.InitiatorName,
                    opt => opt.MapFrom(r => $"{userManager.FindByIdAsync(r.InitiatorId).Result.FirstName} " +
                                            $" {userManager.FindByIdAsync(r.InitiatorId).Result.LastName}"))

                .ForMember(dest => dest.ResolverName, opt => opt.MapFrom(r =>
                    $"{userManager.FindByIdAsync(r.ResolverId).Result.FirstName} " +
                    $" {userManager.FindByIdAsync(r.ResolverId).Result.LastName}"));

            CreateMap<CreateRequestViewModel, Request>();
        }
    }
}