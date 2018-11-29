using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IBodyTypeService
    {
        BodyType GetBodyType(string bodyName);

        BodyType GetBodyType(int id);

        IList<BodyType> GetBodyTypes();
    }
}
