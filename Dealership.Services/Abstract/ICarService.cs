using Dealership.Data.CompositeModels;
using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dealership.Services.Abstract
{
    public interface ICarService
    {
        Car AddCar(int brandId, int carModelId, int mileage, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, int bodyTypeId, string colorName, int colorTypeId,
            int fuelTypeId, int gearBoxTypeId, byte numberOfGears, ICollection<int> extrasIds);

        Task<Car> GetCarAsync(int id);

        Task<Car> RemoveCarAsync(int carId);

        CarSearchResult GetCarSearchResult(int brandId, int modelId, int sortKey, int page);

        int GetAllCarsCount();

        Car Update(Car car);

        void SaveImages(string root, IList<string> fileNames, IList<Stream> stream, int carId);
    }
}