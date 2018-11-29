using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Web.Tests.EditCarService
{
    [TestClass]
    public class EditModel_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            //arrange

            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
              .UseInMemoryDatabase(databaseName:
              "EditModelCorrectly_WhenValidParametersArePassed").Options;

            IEditCarService sut;
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);
            }

            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.EditModel(null));
        }
        [TestMethod]
        public void ThrowArgumentNullException_WhenNoArgumentArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
              .UseInMemoryDatabase(databaseName:
              "EditModelCorrectly_WhenValidParametersArePassed").Options;

            IEditCarService sut;
            var emptyArray = new string[3];
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);
            }

            //act&assert
            Assert.ThrowsException<ArgumentException>(() => sut.EditModel(emptyArray));
        }

        //[TestMethod]
        //public void EditModelCorrectly_WhenValidParametersArePassed()
        //{
        //    var validParameters = new string[2] { "1", "330xi" };
        //    var testCar = new Car() { CarModel = new CarModel() { Name = validParameters[1] }
        //    ,BodyTypeId = 1,BrandId = 1,CarModelId = 1,ColorId = 1,FuelTypeId = 1,GearBoxId = 1,
        //    ProductionDate = DateTime.Now
        //    };

        //    string result;

        //    var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
        //       .UseInMemoryDatabase(databaseName:
        //       "EditModelCorrectly_WhenValidParametersArePassed").Options;

        //    using (var dealershipContext = new DealershipContext(contextOptions))
        //    {
        //        dealershipContext.SaveChangesAsync();
        //        var extrasIds = testCar.CarsExtras.Select(x => x.ExtraId).ToList();

        //        var extraService = new Services.ExtraService(dealershipContext);

        //        var carService = new Services.CarService(dealershipContext, extraService);

        //        carService.AddCar(testCar.Id, testCar.CarModelId, testCar.Mileage, testCar.HorsePower,
        //        testCar.EngineCapacity, testCar.ProductionDate, testCar.Price, testCar.BodyTypeId,
        //        testCar.Color.Name, testCar.Color.ColorTypeId, testCar.FuelTypeId, testCar.GearBoxId,
        //        testCar.GearBox.NumberOfGears, extrasIds);

        //        var car = carService.GetCarAsync(testCar.Id).Result;

        //        var sut = new Services.EditCarService(dealershipContext, carService);

        //        result = sut.EditModel(validParameters);
        //    }
        //    //assert    
        //    Assert.IsTrue(result.Contains("edited"));
        //    Assert.IsTrue(testCar.CarModel.Name == validParameters[1]);
        //}
    }
}
