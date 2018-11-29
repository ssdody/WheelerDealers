using System;
using System.Collections.Generic;

namespace Dealership.Data.Models
{
    public interface ICar
    {
        int Id { get; }
        BodyType BodyType { get; set; }
        int BodyTypeId { get; set; }
        Brand Brand { get; set; }
        int BrandId { get; set; }
        CarModel CarModel { get; set; }
        int CarModelId { get; set; }
        IEnumerable<CarsExtras> CarsExtras { get; set; }
        Color Color { get; set; }
        int ColorId { get; set; }
        short EngineCapacity { get; set; }
        FuelType FuelType { get; set; }
        int FuelTypeId { get; set; }
        Gearbox GearBox { get; set; }
        int GearBoxId { get; set; }
        short HorsePower { get; set; }
        ICollection<Image> Images { get; set; }
        bool IsSold { get; set; }
        int Mileage { get; set; }
        decimal Price { get; set; }
        DateTime ProductionDate { get; set; }
        ICollection<UsersCars> UsersCars { get; set; }
    }
}