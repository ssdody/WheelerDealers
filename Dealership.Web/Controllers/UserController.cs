using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Dealership.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddToFavorites(int id)
        {
            var user = this.userManager.GetUserAsync(HttpContext.User).Result;
            this.userService.AddCarToFavorites(id, user);

            return RedirectToAction("Details", "Car", new { id });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Favorites()
        {
            var user = this.userManager.GetUserAsync(HttpContext.User).Result;
            var cars = this.userService.GetFavorites(user);

            var model = cars.Select(c => new CarSummaryViewModel(c)
            {
                Id = c.Id,
                Brand = c.Brand.Name,
                CarModel = c.CarModel.Name,
                Capacity = c.EngineCapacity,
                GearType = c.GearBox.GearType.Name,
                Fuel = c.FuelType.Name,
                Color = c.Color.Name,
                Price = $"{c.Price}$",
                Mileage = $"{c.Mileage} miles"
            });

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult RemoveFromFavorites(int id)
        {
            var user = this.userManager.GetUserAsync(HttpContext.User).Result;
            this.userService.RemoveCarFromFavorites(id, user);

            return RedirectToAction("Details", "Car", new { id });
        }
    }
}