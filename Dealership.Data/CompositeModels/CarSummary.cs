using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Data.CompositeModels
{
    public class CarSummary
    {
        public CarSummary(Car car)
        {
            this.Id = car.Id;
            this.Brand = car.Brand.Name;
            this.CarModel = car.CarModel.Name;
            this.Capacity = car.EngineCapacity;
            this.GearType = car.GearBox.GearType.Name;
            this.Fuel = car.FuelType.Name;
            this.Color = $"{car.Color.ColorType.Name} {car.Color.Name}";
            this.Price = car.Price.ToString();
            this.Mileage = car.Mileage.ToString();
            this.ImageUrl = car.Images.FirstOrDefault() == null ? "default.jpg" : car.Images.FirstOrDefault().ImageName;
        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public string CarModel { get; set; }
        public int Capacity { get; set; }
        public string GearType { get; set; }
        public string Fuel { get; set; }
        public string Color { get; set; }
        public string Price { get; set; }
        public string Mileage { get; set; }
        public string ImageUrl { get; set; }
        //    public ICollection<string> ImagesUrl { get; set; }
    }
}
