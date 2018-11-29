using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IGearTypeService
    {
        Gearbox GetGearBox(int typeId, int numberOfGears);

        IList<GearType> GetGearTypes();

        IList<Gearbox> GetGearboxesDependingOnGearType(int id);
    }
}