using Dealership.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class Car : Entity, ICar
    {
        public Car()
        {
            this.CarsExtras = new HashSet<CarsExtras>();
            this.UsersCars = new HashSet<UsersCars>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        public int Mileage { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public short HorsePower { get; set; }

        [Required]
        [Range(1, 100000)]
        public short EngineCapacity { get; set; }
        public bool IsSold { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ProductionDate { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }

        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }

        public int GearBoxId { get; set; }
        public Gearbox GearBox { get; set; }

        public ICollection<Image> Images { get; set; }

        public IEnumerable<CarsExtras> CarsExtras { get; set; }

        public ICollection<UsersCars> UsersCars { get; set; }
    }
}
