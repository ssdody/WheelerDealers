//using Dealership.Data.Context;
//using Dealership.Data.Models;
//using Dealership.Services.Abstract;
//using Dealership.Services.Exceptions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Dealership.Web.Tests.CarService
//{
//    [TestClass]
//    public class RemoveCar_Should
//    {
//        //[TestMethod]
//        //public void RemoveCarSuccessfully_WhenValidParamatersArePassed()
//        //{
//        //    // Arrange 
//        //    var contexOptions = new DbContextOptionsBuilder<DealershipContext>()
//        //        .UseInMemoryDatabase(databaseName: "RemoveCarSuccessfully_WhenValidParamatersArePassed").Options;

//        //    int carId = 1;
//        //    var car = new Car();

//        //    ICarService sut;

//        //    //assert
//        //    using (var dealershipContext = new DealershipContext(contexOptions))
//        //    {
//        //        sut = new Services.CarService(dealershipContext);
//        //        dealershipContext.Cars.Add(car);
//        //        dealershipContext.SaveChanges();
//        //        sut.RemoveCar(car.Id);

//        //        Assert.ThrowsException<ServiceException>(() => sut.GetCar(carId));
//        //    }
//        //}
//        [TestMethod]
//        public void DeleteCarExtra_WhenValidParametersArePassed()
//        {
//            // Arrange
//            var unitOfWorkMock = new Mock<IUnitOfWork>();

//            int carId = 1;
//            var car = new Car() { Id = carId };
//            var cars = new List<Car>() { car };

//            var userCar = new UsersCars() { CarId = car.Id, UserId = 1 };
//            var usersCars = new List<UsersCars>() { userCar };

//            var carExtra = new CarsExtras() { CarId = car.Id, ExtraId = 1 };
//            var carsExtras = new List<CarsExtras>() { carExtra };

//            unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());

//            unitOfWorkMock.Setup(u => u.GetRepository<UsersCars>().All()).Returns(usersCars.AsQueryable());

//            unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(carsExtras.AsQueryable());

//            var sut = new Services.CarService(unitOfWorkMock.Object);

//            // Act
//            sut.RemoveCar(carId);

//            // Assert
//            unitOfWorkMock.Verify(u => u.GetRepository<CarsExtras>().Delete(It.IsAny<CarsExtras>()), Times.Once);

//            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
//        }

//        //[TestMethod]
//        //public void DeleteUserCar_WhenValidParametersArePassed()
//        //{
//        //    // Arrange
//        //    var unitOfWorkMock = new Mock<IUnitOfWork>();

//        //    int carId = 1;
//        //    var car = new Car() { Id = carId };
//        //    var cars = new List<Car>() { car };

//        //    var userCar = new UsersCars() { CarId = car.Id, UserId = 1 };
//        //    var usersCars = new List<UsersCars>() { userCar };

//        //    var carExtra = new CarsExtras() { CarId = car.Id, ExtraId = 1 };
//        //    var carsExtras = new List<CarsExtras>() { carExtra };

//        //    unitOfWorkMock.Setup(u => u.GetRepository<Car>().All()).Returns(cars.AsQueryable());

//        //    unitOfWorkMock.Setup(u => u.GetRepository<UsersCars>().All()).Returns(usersCars.AsQueryable());

//        //    unitOfWorkMock.Setup(u => u.GetRepository<CarsExtras>().All()).Returns(carsExtras.AsQueryable());

//        //    var sut = new CarService(unitOfWorkMock.Object);

//        //    // Act
//        //    sut.RemoveCar(carId);

//        //    // Assert
//        //    unitOfWorkMock.Verify(u => u.GetRepository<UsersCars>().Delete(It.IsAny<UsersCars>()), Times.Once);

//        //    unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
//        //}
//    }
//    }
//}
