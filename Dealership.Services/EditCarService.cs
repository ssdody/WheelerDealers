using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Services
{
    public class EditCarService : IEditCarService
    {
        private readonly ICarService carService;
        private readonly DealershipContext context;

        public EditCarService(DealershipContext context, ICarService carService)
        {
            if (carService == null)
            {
                throw new ArgumentNullException("CarService cannot be null!");
            }
            if (context == null)
            {
                throw new ArgumentNullException("CarService cannot be null!");
            }
            this.context = context;
            this.carService = carService;
        }

        public async Task<string> EditBrand(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            int id;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            string newValue = parameters[1];

            string secondNewValue = "";
            if (parameters.Length == 3)
            {
                secondNewValue = parameters[2];
            }

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);

            Brand newBrand = this.context.Brands.FirstOrDefault(b => b.Name == newValue);
            car.Brand = newBrand ?? throw new InvalidOperationException();

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Brand of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditModel(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }
            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);

            var model = car.Brand.CarModels.FirstOrDefault(m => m.Name == newValue);
            if (model == null)
            {
                model = new CarModel() { Name = newValue, BrandId = car.Brand.Id };
                this.context.CarModels.Add(model);
            }

            car.CarModel = model;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Model of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditHorsePower(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            car.HorsePower = short.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Horse power of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditEngineCapacity(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            car.EngineCapacity = short.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Engine capacity of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditIsSold(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            car.IsSold = bool.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"IsSold of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditPrice(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            car.Price = decimal.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Price of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditMileAge(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            if (!int.TryParse(parameters[1], out int newValue))
            {
                throw new ArgumentException("Invalid mileage value!");
            }
            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            car.Mileage = newValue;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Mileage of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditProductionDate(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            car.ProductionDate = DateTime.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Production date of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditBodyType(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var newBodyType = this.context.BodyTypes.FirstOrDefault(ch => ch.Name == newValue);

            if (newBodyType == null)
            {
                throw new ArgumentException("Invalid body type!");
            }

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            car.BodyType = newBodyType;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Body type of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditColor(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newColorValue = parameters[1];
            string newColorTypeName = "";

            if (parameters.Length == 3)
            {
                newColorTypeName = parameters[2];
            }
            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);

            var newType = this.context.ColorTypes.FirstOrDefault(gt => gt.Name == newColorTypeName);
            if (newType == null)
            {
                throw new ArgumentException("Invalid color type!");
            }

            var newColor = this.context.Colors
                                     .Include(c => c.ColorType)
                                     .FirstOrDefault(c => c.Name == newColorValue
                                     && c.ColorType.Name == newColorTypeName);

            if (newColor == null)
            {
                newColor = new Color() { Name = newColorValue, ColorType = newType };

                this.context.Colors.Add(newColor);
                this.context.SaveChanges();
            }

            car.ColorId = newColor.Id;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Color of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditColorType(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);
            var colorName = car.Color.Name;
            var newColorType = this.context.ColorTypes.FirstOrDefault(ct => ct.Name == newValue);
            if (newColorType == null) { throw new ArgumentNullException("Color type not exist!"); }

            var newColor = this.context.Colors
                .Include(c => c.ColorType)
                .FirstOrDefault(c => c.Name == colorName
                && c.ColorType.Name == newValue);
            if (newColor == null)
            {
                newColor = new Color { Name = colorName, ColorType = newColorType };
                this.context.Colors.Add(newColor);
                this.context.SaveChanges();
            }

            car.Color = newColor;
            car.ColorId = newColor.Id;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Color type of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public async Task<string> EditFuelType(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);

            var newFuelType = this.context.FuelTypes.FirstOrDefault(ft => ft.Name == newValue);

            if (newFuelType != null)
            {
                car.FuelType = newFuelType;
                this.context.Cars.Update(car);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Fuel type :{newValue} not exist!");
            }

            return $"Fuel type of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";

        }

        public async Task<string> EditGearbox(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = await this.carService.GetCarAsync(id).ConfigureAwait(false);

            GearType newGearType = this.context.GearTypes.First(gt => gt.Name == newValue);

            if (newGearType == null)
            {
                throw new ArgumentException($"Gearbox:{newValue} not exist!");
            }
            car.GearBox.GearType = newGearType;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Gearbox of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }
    }
}
