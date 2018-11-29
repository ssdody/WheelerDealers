using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.UserServiceTests
{
    [TestClass]
    public class RemoveCarFromFavorites_Should
    {
        [TestMethod]
        public void RemoveCar_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            int carId = 1;

            var user = new User() { Username = username, };
            var users = new List<User>() { user };

            var car = new Car() { Id = 1 };
            var cars = new List<Car>() { car };
            
            var userCar = new UsersCars() { CarId = car.Id, User = user };
            var usersCars = new List<UsersCars>() { userCar };

            carServiceMock.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);
            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(users.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<UsersCars>().All()).Returns(usersCars.AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act
            sut.RemoveCarFromFavorites(carId, username);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<UsersCars>().Delete(It.IsAny<UsersCars>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void ThrowServiceException_WhenCarIsNotInFavorites()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            int carId = 1;

            var user = new User() { Username = username, };
            var users = new List<User>() { user };

            var car = new Car() { Id = 1 };
            var cars = new List<Car>() { car };

            var usersCars = new List<UsersCars>();

            carServiceMock.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);
            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(users.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<UsersCars>().All()).Returns(usersCars.AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ServiceException>(() => sut.RemoveCarFromFavorites(carId, username));
        }
    }
}
