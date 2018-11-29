using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IBrandService
    {
        Brand GetBrand(string brandName);

        Brand Add(Brand newBrand);

        Brand GetBrand(int brandId);

        IList<CarModel> GetAllModelsForBrand(int brandId);

        IList<Brand> GetBrands();

        Brand Create(string brandName);

        CarModel GetModeldOfBrand(int brandId, int modelId);
    }
}
