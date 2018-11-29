using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class GearTypeService : IGearTypeService
    {
        private readonly DealershipContext context;

        public GearTypeService(DealershipContext context)
        {
            this.context = context;
        }

        public Gearbox GetGearBox(int typeId, int numberOfGears)
        {

            var type = this.context.Gearboxes.FirstOrDefault(gb => gb.GearTypeId == typeId && gb.NumberOfGears == numberOfGears);
            if (type == null)
            {
                throw new ServiceException($"There is no such gearbox.");
            }
            return type;
        }

        public IList<GearType> GetGearTypes()
        {
            return this.context.GearTypes.ToList();
        }

        public IList<Gearbox> GetGearboxesDependingOnGearType(int id)
        {
            return this.context.Gearboxes.Where(g => g.GearTypeId == id).ToList();
        }

        public IList<GearType> GetNumberOfGearsTypes()
        {
            return this.context.GearTypes.ToList();
        }
    }
}
