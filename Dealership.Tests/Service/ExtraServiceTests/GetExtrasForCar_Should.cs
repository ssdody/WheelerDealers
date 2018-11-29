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
    public class GetExtrasForCar_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenInvalidIdIsPassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            var invalidId = -1;
            var car = new Car() { Id = 1 };
            var list = new List<Car>() { car };
            carServiceMock.Setup(c => c.GetCar(It.IsAny<int>())).Returns(car);
            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(list.AsQueryable());

            var sut = new ExtraService(unitOfWorkMock.Object);

            // Act   && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.GetExtrasForCar(invalidId));
        }


        [TestMethod]
        public void ReturnAListOfExtras_WhenValidParametersArePassed()
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

            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(listOfCars.AsQueryable());

            var sut = new ExtraService(unitOfWorkMock.Object);
            // Act
            var actual = sut.GetExtrasForCar(carId);

            // Assert
            Assert.AreEqual(listExtras.First().Id, actual.First().Id);
        }
    }
}
