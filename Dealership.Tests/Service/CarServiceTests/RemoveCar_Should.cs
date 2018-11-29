using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.CarServiceTests
{
    [TestClass]
    public class RemoveCar_Should
    {
        [TestMethod]
        public void DeleteCar_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            int carId = 1;
            var car = new Car() { Id = carId };
            var cars = new List<Car>() { car };

            var userCar = new UsersCars() { CarId = car.Id, UserId = 1 };
            var usersCars = new List<UsersCars>() { userCar };

            var carExtra = new CarsExtras() { CarId = car.Id, ExtraId = 1 };
            var carsExtras = new List<CarsExtras>() { carExtra };

            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());

            unitOfWorkMock.Setup(u => u.GetRepository<UsersCars>().All()).Returns(usersCars.AsQueryable());

            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(carsExtras.AsQueryable());

            var sut = new CarService(unitOfWorkMock.Object);

            // Act
            sut.RemoveCar(carId);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<Car>().Delete(It.IsAny<Car>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteCarExtra_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            int carId = 1;
            var car = new Car() { Id = carId };
            var cars = new List<Car>() { car };

            var userCar = new UsersCars() { CarId = car.Id, UserId = 1 };
            var usersCars = new List<UsersCars>() { userCar };

            var carExtra = new CarsExtras() { CarId = car.Id, ExtraId = 1 };
            var carsExtras = new List<CarsExtras>() { carExtra };

            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());

            unitOfWorkMock.Setup(u => u.GetRepository<UsersCars>().All()).Returns(usersCars.AsQueryable());

            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(carsExtras.AsQueryable());

            var sut = new CarService(unitOfWorkMock.Object);

            // Act
            sut.RemoveCar(carId);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<CarsExtras>().Delete(It.IsAny<CarsExtras>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteUserCar_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            int carId = 1;
            var car = new Car() { Id = carId };
            var cars = new List<Car>() { car };

            var userCar = new UsersCars() { CarId = car.Id, UserId = 1 };
            var usersCars = new List<UsersCars>() { userCar };

            var carExtra = new CarsExtras() { CarId = car.Id, ExtraId = 1 };
            var carsExtras = new List<CarsExtras>() { carExtra };

            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());

            unitOfWorkMock.Setup(u => u.GetRepository<UsersCars>().All()).Returns(usersCars.AsQueryable());

            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(carsExtras.AsQueryable());

            var sut = new CarService(unitOfWorkMock.Object);

            // Act
            sut.RemoveCar(carId);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<UsersCars>().Delete(It.IsAny<UsersCars>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}