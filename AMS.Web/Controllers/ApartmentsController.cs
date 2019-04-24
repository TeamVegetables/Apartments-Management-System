using System.Threading.Tasks;
using AMS.Core.Models;
using AMS.Services.Interfaces;
using AMS.Web.ViewModels.Apartments;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Web.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IApartmentService apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            this.apartmentService = apartmentService;
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
                    Busy = createApartmentViewModel.Busy,
                    Capacity = createApartmentViewModel.Capacity,
                    Status = createApartmentViewModel.Status,
                    Title = createApartmentViewModel.Title
                };
                await apartmentService.AddApartmentAsync(apartment);

                return RedirectToAction("Index");
            }

            return View(createApartmentViewModel);
        }
    }
}
