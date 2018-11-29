using System.Collections.Generic;
using System.Threading.Tasks;
using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IUserService
    {
        Task<Car> AddCarToFavorites(int carId, User user);

        IList<Car> GetFavorites(User user);

        ICollection<User> GetUsers();

        bool IsCarFavorite(int carId, User user);

        Task<Car> RemoveCarFromFavorites(int carId, User user);
    }
}