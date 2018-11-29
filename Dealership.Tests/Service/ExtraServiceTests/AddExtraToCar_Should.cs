using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.ExtraServiceTests
{
    [TestClass]
    public class AddExtraToCar_Should
    {
        [TestMethod]
        public void AddExtraToCar_WhenValidParametersArePassedAndExtraExists()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string extraName = "testExtra";
            var extra = new Extra() { Id = 1, Name = extraName };
            var carId = 1;
            var car = new Car() { Id = carId };
            var carExtra = new CarsExtras() { CarId = carId, ExtraId = extra.Id };
            var listOfCars = new List<Car>() { car };
            var listCarsExtras = new List<CarsExtras>() { carExtra };
            var listExtras = new List<Extra>() { extra };

            carServiceMock.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);
            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(listOfCars.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(listCarsExtras.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<Extra>().All()).Returns(listExtras.AsQueryable());

            var sut = new ExtraService(unitOfWorkMock.Object);

            // Act
            sut.AddExtraToCar(carId, extraName);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<CarsExtras>().Add(It.IsAny<CarsExtras>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void AddExtraToCar_WhenValidParametersArePassedAndExtraDoesNotExists()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string extraName = "testExtra";
            var carId = 1;
            var car = new Car() { Id = carId };
            var carExtra = new CarsExtras();
            var listOfCars = new List<Car>() { car };
            var listCarsExtras = new List<CarsExtras>();
            var listExtras = new List<Extra>();

            carServiceMock.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);
            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(listOfCars.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(listCarsExtras.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<Extra>().All()).Returns(listExtras.AsQueryable());

            var sut = new ExtraService(unitOfWorkMock.Object);

            // Act
            sut.AddExtraToCar(carId, extraName);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<CarsExtras>().Add(It.IsAny<CarsExtras>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Exactly(2));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenInvalidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string extraName = "testExtra";
            var carId = 1;
            var car = new Car() { Id = carId };
            var carExtra = new CarsExtras();
            var listOfCars = new List<Car>() { car };
            var listCarsExtras = new List<CarsExtras>();
            var listExtras = new List<Extra>();

            carServiceMock.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);
            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(listOfCars.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(listCarsExtras.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<Extra>().All()).Returns(listExtras.AsQueryable());

            var invalidCarId = -1;
            var sut = new ExtraService(unitOfWorkMock.Object);

            // Act   && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.AddExtraToCar(invalidCarId, extraName));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenCarAlreadyContainsExtra()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string extraName = "testExtra";
            var extraId = 1;
            var carId = 1;

            var carExtra = new CarsExtras() { CarId = carId, ExtraId = extraId };

            var extra = new Extra() { Id = extraId, Name = extraName };
            var car = new Car() { Id = carId };

            car.CarsExtras.Add(carExtra);
            extra.CarsExtras.Add(carExtra);
            carExtra.Car = car;
            carExtra.Extra = extra;

            var listOfCars = new List<Car>() { car };
            var listExtras = new List<Extra>() { extra };
            var listCarsExtras = new List<CarsExtras>() { carExtra };

            carServiceMock.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);
            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(listOfCars.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(listCarsExtras.AsQueryable());
            unitOfWorkMock.Setup(u => u.GetRepository<Extra>().All()).Returns(listExtras.AsQueryable());

            var sut = new ExtraService(unitOfWorkMock.Object);

            // Act   && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.AddExtraToCar(carId, extraName));
        }
    }
}
