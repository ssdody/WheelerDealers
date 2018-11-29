using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IFuelTypeService
    {
        IList<FuelType> GetFuelTypes();

        FuelType GetFuelType(int id);
    }
}