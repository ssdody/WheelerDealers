using Dealership.Data.CompositeModels;
using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Models.CarViewModels
{
    public class CarSummaryViewModel
    {
        public CarSummaryViewModel()
        {
        }

        public CarSummaryViewModel(Car car)
        {
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
    }
}
