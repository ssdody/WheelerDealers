using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Services
{
    public class UserService : IUserService
    {
        private readonly ICarService carService;
        private readonly DealershipContext dealershipContext;

        public UserService(ICarService carService, DealershipContext dealershipContext)
        {
            this.carService = carService;
            this.dealershipContext = dealershipContext;
        }

        public ICollection<User> GetUsers()
        {
            var users = this.dealershipContext.Users.Include(u => u.UsersCars)
                                                    .ToList();
            return users;
        }

        public async Task<Car> AddCarToFavorites(int carId, User user)
        {
            Car car = await this.carService.GetCarAsync(carId).ConfigureAwait(false);

            var isCarFavorite = IsCarFavorite(carId, user);

            if (!isCarFavorite)
            {
                var newUserCar = new UsersCars() { CarId = carId, User = user };
                this.dealershipContext.UsersCars.Add(newUserCar);
                this.dealershipContext.SaveChanges();
            }

            return car;
        }

        public async Task<Car> RemoveCarFromFavorites(int carId, User user)
        {
            Car car = await this.carService.GetCarAsync(carId).ConfigureAwait(false);

            var usersCars = this.dealershipContext.UsersCars.FirstOrDefault(uc => uc.CarId == carId && uc.User == user);

            if (usersCars != null)
            {
                this.dealershipContext.UsersCars.Remove(usersCars);
                this.dealershipContext.SaveChanges();
            }

            return car;
        }

        public IList<Car> GetFavorites(User user)
        {
            var userCars = this.dealershipContext.Users
                                        .Include(u => u.UsersCars)
                                        .ThenInclude(uc => uc.Car)
                                        .FirstOrDefault(u => u == user)
                                        .UsersCars;

            var cars = new List<Car>();
            foreach (var uc in userCars.Where(uc => uc.IsDeleted == false))
            {
                var car = this.carService.GetCarAsync(uc.CarId).Result;
                cars.Add(car);
            }

            return cars;
        }

        public bool IsCarFavorite(int carId, User user)
        {
            return this.dealershipContext.UsersCars.Any(uc => uc.CarId == carId && uc.UserId == user.Id);
        }
    }
}
