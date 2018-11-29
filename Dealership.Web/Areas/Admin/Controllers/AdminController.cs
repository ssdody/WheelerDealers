using Dealership.Services.Abstract;
using Dealership.Web.Areas.Admin.Models;
using Dealership.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBrandService brandService;
        private readonly IFuelTypeService fuelTypeService;
        private readonly IColorTypeService colorTypeService;
        private readonly IBodyTypeService bodyTypeService;
        private readonly IGearTypeService gearTypeService;
        private readonly IModelService modelService;
        private readonly ICarService carService;
        private readonly IUserService userService;
        private readonly IExtraService extraService;
        private readonly IColorService colorService;

        public AdminController(IFuelTypeService fuelTypeService, IColorTypeService colorTypeService,
            IBodyTypeService bodyTypeService,
            IGearTypeService gearTypeService, IModelService modelService, IUserService userService, ICarService carService, IBrandService brandService, IExtraService extraService, IColorService colorService)
        {
            this.brandService = brandService;
            this.fuelTypeService = fuelTypeService;
            this.colorTypeService = colorTypeService;
            this.bodyTypeService = bodyTypeService;
            this.gearTypeService = gearTypeService;
            this.modelService = modelService;
            this.userService = userService;
            this.carService = carService;
            this.extraService = extraService;
            this.colorService = colorService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult AddFeatures()
        {
            var model = new AddViewModel()
            {
                Brands = this.brandService.GetBrands()
               .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                StatusMessage = this.StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddExtra(AddViewModel model)
        {
            string extra = model.Extra;
            var newExtra = this.extraService.CreateExtra(extra);
            this.extraService.AddExtra(newExtra);
            this.StatusMessage = "Extra added successfully!";

            return RedirectToAction("AddFeatures");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBrand(AddViewModel model)
        {
            var brand = model.Brand;
            var newBrand = this.brandService.Create(brand);
            this.brandService.Add(newBrand);
            this.StatusMessage = "Brand added successfully!";

            return RedirectToAction("AddFeatures");
        }

        //[HttpGet]
        //public IActionResult AddModel()
        //{
        //    var model = new ModelViewModel()
        //    {
        //        Brands = this.brandService.GetBrands()
        //       .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
        //    };
        //    return RedirectToAction("AddFeatures");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddModel(string model, int brandId)
        {
            //validation todo
            this.modelService.Add(brandId, model);
            this.StatusMessage = "Model added successfully!";

            return RedirectToAction("AddFeatures");
        }


        [HttpGet]
        public IActionResult CreateCar()
        {
            var allExtras = this.extraService.GetAllExtras();

            var brands = this.brandService.GetBrands();
            List<SelectListItem> carModelListItems;

            if (brands.Count != 0)
            {
                var models = this.modelService.GetAllModelsByBrandId(brands.First().Id).ToList();
                carModelListItems = models.Count != 0
                    ? models.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList()
                    : new List<SelectListItem>();
            }
            else { carModelListItems = new List<SelectListItem>(); }


            var model = new EditCarViewModel
            {
                Brands = brands.Count != 0
               ? brands.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()
               : new List<SelectListItem>(),

                CarModels = carModelListItems,
                NumberOfGears = this.gearTypeService.GetGearboxesDependingOnGearType(this.gearTypeService.GetGearTypes().FirstOrDefault().Id)
                                    .Select(x => new SelectListItem { Value = x.NumberOfGears.ToString(), Text = x.NumberOfGears.ToString() }).ToList(),

                BodyTypes = this.bodyTypeService.GetBodyTypes()
                                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                GearTypes = this.gearTypeService.GetGearTypes()
                                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                ColorTypes = this.colorTypeService.GetColorTypes()
                                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                FuelTypes = this.fuelTypeService.GetFuelTypes()
                                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                Car = new CarViewModel()
                {
                    StatusMessage = this.StatusMessage
                },

                Extras = allExtras.Select(e => new ExtraCheckBox
                {
                    Id = e.Id,
                    Name = e.Name,
                    Selected = false
                }).ToArray()
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCar(EditCarViewModel model)
        {
            var extrasIds = model.Extras.Where(e => e.Selected == true).Select(e => e.Id).ToList();
            var car = this.carService.AddCar(
                           model.Car.BrandId, model.Car.CarModelId, model.Car.Mileage, model.Car.HorsePower,
                           model.Car.EngineCapacity, model.Car.ProductionDate, model.Car.Price,
                           model.Car.BodyTypeId, model.Car.Color, model.Car.ColorTypeId, model.Car.FuelTypeId,
                           model.Car.GearBoxTypeId, model.Car.NumberOfGears, extrasIds);

            if (model.Images != null)
            {
                foreach (var image in model.Images)
                {
                    if (!this.IsValidImage(image))
                    {
                        this.StatusMessage = "Error: Please provide a.jpg or .png file smaller than 5MB";
                        return this.RedirectToAction(nameof(CreateCar));
                    }
                }

                this.carService.SaveImages(
                         this.GetUploadsRoot(),
                         model.Images.Select(i => i.FileName).ToList(),
                         model.Images.Select(i => i.OpenReadStream()).ToList(),
                         car.Id
                     );
            }

            this.StatusMessage = "Car registration is successful!";

            return RedirectToAction("Details", "Car", new { area = "", id = car.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            var allExtras = this.extraService.GetAllExtras();

            var model = new EditCarViewModel
            {
                Brands = this.brandService.GetBrands()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                CarModels = this.modelService.GetAllModelsByBrandId(car.BrandId)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),

                NumberOfGears = this.gearTypeService.GetGearboxesDependingOnGearType(id)
               .Select(x => new SelectListItem { Value = x.NumberOfGears.ToString(), Text = x.NumberOfGears.ToString() }).ToList(),

                BodyTypes = this.bodyTypeService.GetBodyTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                GearTypes = this.gearTypeService.GetGearTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                ColorTypes = this.colorTypeService.GetColorTypes()
                 .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                FuelTypes = this.fuelTypeService.GetFuelTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                Car = new CarViewModel(car)
                {
                    StatusMessage = this.StatusMessage
                },

                Extras = allExtras.Select(e => new ExtraCheckBox
                {
                    Id = e.Id,
                    Name = e.Name,
                    Selected = car.CarsExtras.Any(ce => ce.Extra.Id == e.Id) ? true : false
                }).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCarViewModel model)
        {
            await EditCar(model.Car, model.Extras).ConfigureAwait(false);

            return RedirectToAction("Details", "Car", new { area = "", model.Car.Id });
        }

        //method
        //TODO: Mileage doesn't change... Move this method to carService

        public async Task EditCar(CarViewModel model, IEnumerable<ExtraCheckBox> extrasCB)
        {
            var realCar = await carService.GetCarAsync(model.Id).ConfigureAwait(false);

            var newBody = bodyTypeService.GetBodyType(model.BodyTypeId);

            var newBrand = brandService.GetBrand(model.BrandId);
            var newModel = brandService.GetModeldOfBrand(model.BrandId, model.CarModelId);

            var newColor = this.colorService.GetColor(model.Color, model.ColorTypeId);
            if (newColor == null)
            {
                newColor = this.colorService.AddColor(model.Color, model.ColorTypeId);
            }
            var newEngineCapacity = model.EngineCapacity;
            var newFuelType = fuelTypeService.GetFuelType(model.FuelTypeId);
            var newGearbox = this.gearTypeService.GetGearBox(model.GearBoxTypeId, model.NumberOfGears);
            var newHorsePower = model.HorsePower;
            var newPrice = model.Price;
            var newProductionDate = model.ProductionDate;

            realCar.BodyType = newBody;
            realCar.BodyTypeId = newBody.Id;
            realCar.Brand = newBrand;
            realCar.BrandId = newBrand.Id;
            realCar.Color = newColor;
            realCar.ColorId = newColor.Id;
            realCar.EngineCapacity = newEngineCapacity;
            realCar.FuelType = newFuelType;
            realCar.FuelTypeId = newFuelType.Id;
            realCar.GearBox = newGearbox;
            realCar.GearBoxId = newGearbox.Id;
            realCar.HorsePower = newHorsePower;
            realCar.CarModelId = newModel.Id;
            realCar.CarModel = newModel;
            realCar.Price = newPrice;
            realCar.ProductionDate = newProductionDate;
            realCar.ModifiedOn = DateTime.Now;

            var newExtrasIds = extrasCB.Where(e => e.Selected == true).Select(e => e.Id).ToList();
            var currentExtrasIds = extraService.GetExtrasForCar(model.Id).Select(e => e.Id).ToList();

            var extraIdsToDelete = currentExtrasIds.Except(newExtrasIds).ToList();
            this.extraService.DeleteExtrasFromCar(realCar, extraIdsToDelete);

            var extrasIdsToAdd = newExtrasIds.Except(currentExtrasIds).ToList();
            this.extraService.AddExtrasToCar(realCar, extrasIdsToAdd);

            carService.Update(realCar);
        }

        [HttpGet]
        public IActionResult Delete(bool confirm, int id)
        {
            if (confirm)
            {
                var removedCar = carService.RemoveCarAsync(id).ConfigureAwait(false);
            }
            return RedirectToAction("Browse", "Car", new { area = "" });
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult DoesExtraExist(string extra)
        {
            var extraObj = extraService.GetExtraByName(extra);

            return extraObj == null ? Json(true) : Json($"Extra {extraObj.Name} already exists.");
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult DoesBrandExist(string brand)
        {
            var brandObj = brandService.GetBrand(brand);

            return brandObj == null ? Json(true) : Json($"Brand {brandObj.Name} already exists.");
        }

        private string GetUploadsRoot()
        {
            var environment = this.HttpContext.RequestServices
                .GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;

            return Path.Combine(environment.WebRootPath, "images", "cars");
        }

        private bool IsValidImage(IFormFile image)
        {
            string type = image.ContentType;
            if (type != "image/png" && type != "image/jpg" && type != "image/jpeg")
            {
                return false;
            }
            return image.Length <= 5242880;
        }
    }
}