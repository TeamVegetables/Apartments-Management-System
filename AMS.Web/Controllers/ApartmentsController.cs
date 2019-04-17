using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS.Web.Controllers
{
    public class ApartmentsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Apartment> apartments = new List<Apartment>();
            apartments.Add(new Apartment {
                Title = ("Test apartment"),
                Capacity = 3,
                ApartmentStatus = new ApartmentStatus { Title = "Status"}
            });
            apartments.Add(new Apartment
            {
                Title = ("Test apartment"),
                Capacity = 3,
                ApartmentStatus = new ApartmentStatus { Title = "Status" }
            });
            apartments.Add(new Apartment
            {
                Title = ("Test apartment"),
                Capacity = 3,
                ApartmentStatus = new ApartmentStatus { Title = "Status" }
            });
            return View(apartments);
        }
    }
}
