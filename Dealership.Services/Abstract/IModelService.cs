using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IModelService
    {
        ICollection<CarModel> GetAllModelsByBrandId(int brandId);

        CarModel GetModel(int id);

        void Add(int brandId, string modelName);
    }
}