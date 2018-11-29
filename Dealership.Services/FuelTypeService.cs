using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class FuelTypeService : IFuelTypeService
    {
        private readonly DealershipContext context;

        public FuelTypeService(DealershipContext context)
        {
            this.context = context;
        }

        public FuelType GetFuelType(int id)
        {
            var type = this.context.FuelTypes.Find(id);
            if (type == null)
            {
                throw new ServiceException($"There is no fuelType with id {id}.");
            }
            return type;
        }

        public IList<FuelType> GetFuelTypes()
        {
            return this.context.FuelTypes.ToList();
        }
    }
}
