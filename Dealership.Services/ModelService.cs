using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class ModelService : IModelService
    {
        private readonly DealershipContext context;
        private readonly IBrandService brandService;

        public ModelService(DealershipContext context, IBrandService brandService)
        {
            this.context = context;
            this.brandService = brandService;
        }

        public ICollection<CarModel> GetAllModelsByBrandId(int brandId)
        {
            return this.brandService.GetBrand(brandId).CarModels;
        }

        public CarModel GetModel(int id)
        {
            return this.context.CarModels.FirstOrDefault(m => m.Id == id);
        }

        public void Add(int brandId, string modelName)
        {
            var model = this.GetAllModelsByBrandId(brandId).FirstOrDefault(m => m.Name == modelName);

            if (model != null)
            {
                throw new ServiceException($"Model {modelName} is already added!");
            }

            var newModel = new CarModel() { BrandId = brandId, Name = modelName };

            this.context.CarModels.Add(newModel);
            this.context.SaveChanges();
        }
    }
}
