using Dealership.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dealership.Web.Models
{
    public class CarViewModel
    {
        private DateTime productionDate;

        public CarViewModel()
        {
        }

        public CarViewModel(Car car)
        {
            this.Id = car.Id;
            this.CarModelId = car.CarModelId;
            this.CarModel = car.CarModel.Name;
            this.HorsePower = car.HorsePower;
            this.Mileage = car.Mileage;
            this.EngineCapacity = car.EngineCapacity;
            this.Price = car.Price;
            this.BodyTypeId = car.BodyTypeId;
            this.BodyType = car.BodyType.Name;
            this.BrandId = car.BrandId;
            this.Brand = car.Brand.Name;
            this.Color = car.Color.Name;
            this.ColorTypeId = car.Color.ColorTypeId;
            this.ColorType = car.Color.ColorType.Name;
            this.ProductionDate = car.ProductionDate;
            this.GearBoxTypeId = car.GearBox.GearTypeId;
            this.GearBoxType = car.GearBox.GearType.Name;
            this.NumberOfGears = car.GearBox.NumberOfGears;
            this.FuelTypeId = car.FuelTypeId;
            this.FuelType = car.FuelType.Name;
            this.ImagesUrl = car.Images.Select(i => i.ImageName).ToList();
            this.CarsExtras = car.CarsExtras.Select(ce => ce.Extra.Name);
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string CarModel { get; set; }
        public int CarModelId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public short HorsePower { get; set; }

        [Required]
        [Range(1, 100000)]
        public short EngineCapacity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate
        {
            get
            {
                return this.productionDate == DateTime.MinValue ? DateTime.Now : productionDate;
            }
            set
            {
                productionDate = value;
            }
        }

        public int BrandId { get; set; }
        public string Brand { get; set; }


        public int BodyTypeId { get; set; }
        public string BodyType { get; set; }


        public string Color { get; set; }

        public string ColorType { get; set; }
        public int ColorTypeId { get; set; }

        public string FuelType { get; set; }
        public int FuelTypeId { get; set; }

        public string GearBoxType { get; set; }
        public int GearBoxTypeId { get; set; }

        public byte NumberOfGears { get; set; }

        public int Mileage { get; set; }

        public IEnumerable<string> CarsExtras { get; set; }

        public ICollection<IFormFile> Images { get; set; }

        public ICollection<string> ImagesUrl { get; set; }

        public string StatusMessage { get; set; }

        public bool IsFavorite { get; set; }
    }
}
