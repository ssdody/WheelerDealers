using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class BrandService : IBrandService
    {
        private readonly DealershipContext context;

        public BrandService(DealershipContext context)
        {
            this.context = context;
        }

        public Brand Add(Brand newBrand)
        {
            this.context.Brands.Add(newBrand);
            this.context.SaveChanges();
            return newBrand;
        }

        public Brand Create(string brandName)
        {
            var brand = this.GetBrand(brandName);

            if (brand != null)
            {
                throw new ServiceException($"There is already brand with name {brandName}.");
            }

            var newBrand = new Brand() { Name = brandName };
            return newBrand;
        }

        public Brand GetBrand(string brandName)
        {
            return this.context.Brands
                                    .Include(b => b.Cars)
                                    .Include(b => b.CarModels)
                                    .FirstOrDefault(b => b.Name == brandName);
        }

        public Brand GetBrand(int brandId)
        {
            var brand = this.context.Brands
                                    .Include(b => b.CarModels)
                                    .FirstOrDefault(b => b.Id == brandId);
            if (brand == null)
            {
                throw new ServiceException($"There is no brand with id {brandId}.");
            }
            return brand;
        }

        public IList<CarModel> GetAllModelsForBrand(int brandId)
        {
            return this.GetBrand(brandId).CarModels.ToList();
        }
        public CarModel GetModeldOfBrand(int brandId, int modelId)
        {
            return this.context.CarModels.FirstOrDefault(m => m.Id == modelId && m.BrandId == brandId);
        }

        public IList<Brand> GetBrands()
        {
            return this.context.Brands.Include(b => b.Cars).Include(b => b.CarModels).ToList();
        }
    }
}
