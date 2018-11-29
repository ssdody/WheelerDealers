using Dealership.Data.CompositeModels;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Models;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IBrandService brandService;
        private readonly IGearTypeService gearTypeService;
        private readonly IModelService modelService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public CarController(ICarService carService, IBrandService brandService,
            IGearTypeService gearTypeService, IModelService modelService,
            IUserService userService, UserManager<User> userManager)
        {
            this.carService = carService;
            this.brandService = brandService;
            this.gearTypeService = gearTypeService;
            this.modelService = modelService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            return StatusCode(404);
        }

        public IActionResult LoadCars(int brandId, int modelId, int sort, int page)
        {
            var cars = this.carService
               .GetCarSearchResult(brandId, modelId, sort, page);

            var nPerPage = 5;
            var totalCount = cars.TotalCount;
            var reminder = totalCount % nPerPage;
            var pageCount = reminder != 0 ? (totalCount / nPerPage) + 1 : totalCount / nPerPage;

            var summaries = this.PopulateSummaries(cars.FoundCars);

            var searchResultVm = new SearchResultViewModel()
            {
                Summaries = summaries,
                NumberOfPages = pageCount,
                CurrentPage = page,
                SelectedBrandId = brandId,
                SelectedModelId = modelId,
                Sort = sort
            };

            // var summaries = this.PopulateSummaries(cars.FoundCars);
            return this.PartialView("_SearchResultPartial", searchResultVm);
        }

        public IActionResult Search(int brandId, int modelId, int sort, int page = 0)
        {
            var cars = this.carService.GetCarSearchResult(brandId, modelId, sort, page);

            var nPerPage = 5;
            var reminder = cars.TotalCount % nPerPage;
            var pageCount = reminder != 0 ? (cars.TotalCount / nPerPage) + 1 : cars.TotalCount / nPerPage;

            var searchVm = new SearchViewModel
            {
                SearchResult = new SearchResultViewModel()
                {
                    Summaries = this.PopulateSummaries(cars.FoundCars),
                    NumberOfPages = pageCount,
                    CurrentPage = 0,
                    SelectedBrandId = brandId,
                    SelectedModelId = modelId,
                    Sort = sort
                },

                Brands = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "All" } },
                CarModels = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "All" } },
                SortCriterias = new List<SelectListItem>() {
                     new SelectListItem { Value = "0", Text = "Published" },
                     new SelectListItem { Value = "1", Text = "Price Ascending" },
                     new SelectListItem { Value = "2", Text = "Price Descending" },
                     },
            };

            searchVm.Brands.AddRange(this.brandService.GetBrands()
                           .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList());

            return this.View(searchVm);
        }

        private IEnumerable<CarSummaryViewModel> PopulateSummaries(IEnumerable<CarSummary> cars)
        {
            return cars.Select(c => new CarSummaryViewModel()
            {
                Id = c.Id,
                Brand = c.Brand,
                CarModel = c.CarModel,
                Capacity = c.Capacity,
                GearType = c.GearType,
                Fuel = c.Fuel,
                Color = c.Color,
                Price = c.Price,
                Mileage = c.Mileage,
                ImageUrl = c.ImageUrl
            });
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public JsonResult GetModelsByBrandId(int brandId)
        {
            var list = this.modelService.GetAllModelsByBrandId(brandId);
            return Json(list);
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public JsonResult GetGearsDependingOnGearBoxType(int id)
        {
            var list = (this.gearTypeService.GetGearboxesDependingOnGearType(id));
            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            var user = await this.userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            CarViewModel model;

            if (user == null)
            {
                model = new CarViewModel(car)
                {
                    StatusMessage = this.StatusMessage
                };
            }
            else
            {
                model = new CarViewModel(car)
                {
                    IsFavorite = userService.IsCarFavorite(id, user),
                    StatusMessage = this.StatusMessage
                };
            }


            return this.View(model);
        }
    }
}