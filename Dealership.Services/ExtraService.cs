using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class ExtraService : IExtraService
    {
        private readonly DealershipContext context;

        public ExtraService(DealershipContext context)
        {
            this.context = context;
        }

        public Extra CreateExtra(string name)
        {
            if (this.context.Extras.Any(e => e.Name == name))
            {
                throw new ArgumentException($"An extra with name {name} already exists!");
            }

            var extra = new Extra() { Name = name };
            return extra;
        }

        public Extra AddExtra(Extra extra)
        {
            this.context.Extras.Add(extra);
            this.context.SaveChanges();
            return extra;
        }

        public Extra AddExtraToCar(int carId, string extraName)
        {
            if (!this.context.Cars.Any(c => c.Id == carId))
            {
                throw new ArgumentException($"Car with Id {carId} does not exist");
            }

            if (this.context.Cars
                                 .Include(c => c.CarsExtras)
                                   .ThenInclude(ce => ce.Extra)
                                 .FirstOrDefault(c => c.Id == carId)
                                 .CarsExtras.Any(ce => ce.Extra.Name == extraName))
            {
                throw new ArgumentException($"Car with Id {carId} already has extra with name {extraName}.");
            }

            var extra = GetExtraByName(extraName);
            if (extra == null)
            {
                extra = new Extra() { Name = extraName };
                this.context.Extras.Add(extra);
                this.context.SaveChanges();
            }

            var newCarExtra = new CarsExtras() { CarId = carId, ExtraId = extra.Id };
            this.context.CarsExtras.Add(newCarExtra);

            this.context.SaveChanges();
            return extra;
        }

        public void AddExtrasToCar(Car car, ICollection<int> extrasIds)
        {
            foreach (var id in extrasIds)
            {
                var newCarExtra = new CarsExtras() { CarId = car.Id, ExtraId = id };
                this.context.CarsExtras.Add(newCarExtra);
            }

            this.context.SaveChanges();
        }

        public void DeleteExtrasFromCar(Car car, ICollection<int> extrasIds)
        {
            foreach (var id in extrasIds)
            {
                var carExtra = this.context.CarsExtras.FirstOrDefault(ce => ce.ExtraId == id && ce.Car == car);
                if (carExtra != null)
                {
                    this.context.CarsExtras.Remove(carExtra);
                }
            }

            this.context.SaveChanges();
        }

        public Extra GetExtraById(int id)
        {
            return this.context.Extras.FirstOrDefault(x => x.Id == id);
        }

        public Extra GetExtraByName(string name)
        {
            return this.context.Extras.FirstOrDefault(e => e.Name == name);
        }

        public ICollection<Extra> GetAllExtras()
        {
            return this.context.Extras.ToList();
        }

        public ICollection<Extra> GetExtrasForCar(int carId)
        {
            if (!this.context.Cars.Any(c => c.Id == carId))
            {
                throw new ArgumentException("Invalid car Id.");
            }
            return this.context.Cars
                                        .Include(c => c.CarsExtras)
                                        .ThenInclude(ce => ce.Extra)
                                        .First(c => c.Id == carId).CarsExtras
                                        .Select(x => x.Extra).ToList();
        }
    }
}
